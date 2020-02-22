using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core.Cache;
using Skyra.Core.Database.Models;
using Skyra.Core.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Spectacles.NET.Broker.Amqp;
using Spectacles.NET.Rest;
using Spectacles.NET.Rest.Bucket;
using Spectacles.NET.Types;
using EventInfo = Skyra.Core.Structures.EventInfo;

namespace Skyra.Core
{
	public class Client
	{
		public Dictionary<string, CommandInfo> Commands { get; private set; }
		public Dictionary<string, EventInfo> Events { get; private set; }
		public Dictionary<string, MonitorInfo> Monitors { get; private set; }
		public Dictionary<Type, ArgumentInfo> Resolvers { get; private set; }

		public Client(ClientOptions clientOptions)
		{
			EventHandler = new EventHandler(this);
			Cache = new CacheClient(clientOptions.RedisPrefix);
			InitializeCaches();

			Token = clientOptions.Token;
			BrokerUri = clientOptions.BrokerUri;
			RedisUri = clientOptions.RedisUri;
			Broker = new AmqpBroker(clientOptions.BrokerName);
			Broker.Receive += (sender, args) =>
			{
				EventHandler.HandleEvent((GatewayEvent) Enum.Parse(typeof(GatewayEvent), args.Event), args);
				Broker.Ack(args.Event, args.DeliveryTag);
			};
		}

		private string Token { get; }
		private string BrokerUri { get; }
		private string RedisUri { get; }
		private AmqpBroker Broker { get; }

		public RestClient Rest { get; private set; }
		public EventHandler EventHandler { get; }
		public CacheClient Cache { get; }

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
		}

		private void InitializeCaches()
		{
			Events = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<EventAttribute>() != null)
				.Select(type => Activator.CreateInstance(type, this))
				.Select(ToEventInfo).ToDictionary(x => x.Name, x => x);

			Monitors = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<MonitorAttribute>() != null)
				.Select(type => Activator.CreateInstance(type, this))
				.Select(ToMonitorInfo).ToDictionary(x => x.Name, x => x);

			Resolvers = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<ResolverAttribute>() != null)
				.Select(Activator.CreateInstance)
				.Select(ToArgumentInfo)
				.ToDictionary(x => x.Type, x => x);

			Commands = Assembly.GetExecutingAssembly()
				.ExportedTypes
				.Where(type => type.GetCustomAttribute<CommandAttribute>() != null)
				.Select(type => Activator.CreateInstance(type, this))
				.Select(ToCommandInfo).ToDictionary(x => x.Name, x => x);
		}

		private static ArgumentInfo ToArgumentInfo(object argument)
		{
			var attribute = argument.GetType().GetCustomAttribute<ResolverAttribute>();

			return new ArgumentInfo
			{
				Instance = argument,
				Method = argument.GetType().GetMethod("ResolveAsync"),
				Type = attribute.Type
			};
		}

		private static EventInfo ToEventInfo(object @event)
		{
			var attribute = @event.GetType().GetCustomAttribute<EventAttribute>();

			return new EventInfo
			{
				Instance = @event,
				Name = attribute.Name ?? @event.GetType().Name
			};
		}

		private static MonitorInfo ToMonitorInfo(object monitor)
		{
			var attribute = monitor.GetType().GetCustomAttribute<MonitorAttribute>();
			var methodInfo = monitor.GetType().GetMethod("RunAsync");
			if (methodInfo == null)
				throw new NullReferenceException($"{nameof(monitor)} does not have a RunAsync method.");

			return new MonitorInfo
			{
				Instance = monitor,
				Method = methodInfo,
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
			var commandInfo = t.GetCustomAttribute<CommandAttribute>();

			return new CommandInfo
			{
				Delimiter = commandInfo.Delimiter,
				Instance = command,
				Name = commandInfo.Name ?? command.GetType().Name.Replace("Command", "").ToLower(),
				Usage = new CommandUsage(this, command)
			};
		}
	}
}
