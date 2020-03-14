using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Skyra.Core.Cache;
using Skyra.Core.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;
using Skyra.Core.Structures.Usage;
using Spectacles.NET.Broker.Amqp;
using Spectacles.NET.Rest;
using Spectacles.NET.Rest.Bucket;
using Spectacles.NET.Types;
using EventInfo = Skyra.Core.Structures.EventInfo;

namespace Skyra.Core
{
	public sealed class Client
	{
		internal Client(ClientOptions clientOptions)
		{
			Language = new CultureLanguage(new[] {"en-US", "es-ES"});
			EventHandler = new EventHandler();
			Cache = new CacheClient(clientOptions.RedisPrefix);

			Id = null;
			Owners = clientOptions.Owners;
			Token = clientOptions.Token;
			BrokerUri = clientOptions.BrokerUri;
			RedisUri = clientOptions.RedisUri;
			Broker = new AmqpBroker(clientOptions.BrokerName);
			Logger = new LoggerConfiguration()
				.Enrich.WithExceptionDetails()
				.WriteTo.Console()
				.CreateLogger();

			Rest = null!;
			Broker.Receive += (sender, args) =>
			{
				EventHandler.HandleEvent((SkyraEvent) Enum.Parse(typeof(SkyraEvent), args.Event), args);
				Broker.Ack(args.Event, args.DeliveryTag);
			};

			var provider = new ServiceCollection()
				.AddSingleton(this)
				.BuildServiceProvider();

			Inhibitors = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<InhibitorAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(provider, type))
				.Select(ToInhibitorInfo).ToDictionary(x => x.Name, x => x);

			Events = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<EventAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(provider, type))
				.Select(ToEventInfo).ToDictionary(x => x.Name, x => x);

			Monitors = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<MonitorAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(provider, type))
				.Select(ToMonitorInfo).ToDictionary(x => x.Name, x => x);

			Resolvers = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<ResolverAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(provider, type))
				.Select(ToArgumentInfo)
				.ToDictionary(x => x.Type, x => x);

			Commands = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<CommandAttribute>() != null)
				.Select(type => ActivatorUtilities.CreateInstance(provider, type))
				.Select(ToCommandInfo).ToDictionary(x => x.Name, x => x);
		}

		public Dictionary<string, InhibitorInfo> Inhibitors { get; }
		public Dictionary<string, CommandInfo> Commands { get; }
		public Dictionary<string, EventInfo> Events { get; }
		public Dictionary<string, MonitorInfo> Monitors { get; }
		public Dictionary<Type, ArgumentInfo> Resolvers { get; }

		private string Token { get; }
		private string BrokerUri { get; }
		private string RedisUri { get; }
		private AmqpBroker Broker { get; }

		public ulong? Id { get; set; }
		public ulong[] Owners { get; set; }
		public RestClient Rest { get; private set; }
		public CultureLanguage Language { get; }
		public EventHandler EventHandler { get; }
		public CacheClient Cache { get; }
		public Logger Logger { get; }

		public async Task ConnectAsync()
		{
			await Cache.ConnectAsync(RedisUri);
			Rest = new RestClient(Token, new RedisBucketFactory(Cache.Pool));
			await Broker.ConnectAsync(new Uri(BrokerUri));
			await Broker.SubscribeAsync(new[]
			{
				"READY",
				"RESUMED",
				"CHANNEL_CREATE",
				"CHANNEL_UPDATE",
				"CHANNEL_DELETE",
				"CHANNEL_PINS_UPDATE",
				"GUILD_CREATE",
				"GUILD_UPDATE",
				"GUILD_DELETE",
				"GUILD_BAN_ADD",
				"GUILD_BAN_REMOVE",
				"GUILD_EMOJIS_UPDATE",
				"GUILD_INTEGRATIONS_UPDATE",
				"GUILD_MEMBER_ADD",
				"GUILD_MEMBER_REMOVE",
				"GUILD_MEMBER_UPDATE",
				"GUILD_MEMBERS_CHUNK",
				"GUILD_ROLE_CREATE",
				"GUILD_ROLE_UPDATE",
				"GUILD_ROLE_DELETE",
				"MESSAGE_CREATE",
				"MESSAGE_UPDATE",
				"MESSAGE_DELETE",
				"MESSAGE_DELETE_BULK",
				"MESSAGE_REACTION_ADD",
				"MESSAGE_REACTION_REMOVE",
				"MESSAGE_REACTION_REMOVE_ALL",
				"TYPING_START",
				"USER_UPDATE",
				"VOICE_STATE_UPDATE",
				"VOICE_SERVER_UPDATE",
				"WEBHOOKS_UPDATE"
			});
			Id = await Cache.GetClientUserAsync();

			// If the owners array is empty, fetch it from application
			if (Owners.Length == 0)
			{
				var application = await Rest.Application.GetAsync<ClientApplication>();
				Owners = application.Team == null
					? new[] {ulong.Parse(application.Owner.Id)}
					: application.Team.Members.Where(x => x.MembershipState == MembershipState.ACCEPTED)
						.Select(x => ulong.Parse(x.User.Id)).ToArray();

				if (Id == null)
				{
					await Cache.SetClientUserAsync(application.Id);
					Id = ulong.Parse(application.Id);
				}
			}
		}

		private static ArgumentInfo ToArgumentInfo(object argument)
		{
			var attribute = argument.GetType().GetCustomAttribute<ResolverAttribute>()!;

			return new ArgumentInfo
			{
				Instance = argument,
				Method = argument.GetType().GetMethod("ResolveAsync")!,
				Type = attribute.Type,
				DisplayName = attribute.DisplayName
			};
		}

		private static InhibitorInfo ToInhibitorInfo(object inhibitor)
		{
			var attribute = inhibitor.GetType().GetCustomAttribute<InhibitorAttribute>()!;

			return new InhibitorInfo
			{
				Name = attribute.Name ?? inhibitor.GetType().Name.Replace("Inhibitor", ""),
				Instance = (IInhibitor) inhibitor
			};
		}

		private static EventInfo ToEventInfo(object @event)
		{
			var attribute = @event.GetType().GetCustomAttribute<EventAttribute>()!;

			return new EventInfo
			{
				Instance = @event,
				Name = attribute.Name ?? @event.GetType().Name
			};
		}

		private static MonitorInfo ToMonitorInfo(object monitor)
		{
			var attribute = monitor.GetType().GetCustomAttribute<MonitorAttribute>()!;
			var methodInfo = monitor.GetType().GetMethod("RunAsync");
			if (methodInfo == null)
			{
				throw new NullReferenceException($"{nameof(monitor)} does not have a RunAsync method.");
			}

			return new MonitorInfo
			{
				Instance = (IMonitor) monitor,
				Name = attribute.Name ?? monitor.GetType().Name,
				AllowedTypes = attribute.AllowedTypes,
				IgnoreBots = attribute.IgnoreBots,
				IgnoreEdits = attribute.IgnoreEdits,
				IgnoreOthers = attribute.IgnoreOthers,
				IgnoreSelf = attribute.IgnoreSelf,
				IgnoreWebhooks = attribute.IgnoreWebhooks
			};
		}

		private CommandInfo ToCommandInfo(object command)
		{
			var t = command.GetType();
			var commandInfo = t.GetCustomAttribute<CommandAttribute>()!;

			return new CommandInfo
			{
				Delimiter = commandInfo.Delimiter,
				Instance = command,
				Name = commandInfo.Name ?? command.GetType().Name.Replace("Command", "").ToLower(),
				Usage = new CommandUsage(this, command),
				FlagSupport = commandInfo.FlagSupport,
				QuotedStringSupport = commandInfo.QuotedStringSupport,
				Inhibitors = commandInfo.Inhibitors.Select(v => Inhibitors[v].Instance).ToArray()
			};
		}
	}
}
