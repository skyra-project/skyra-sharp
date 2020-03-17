using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Exceptions;
using Skyra.Core.Cache;
using Skyra.Core.Models;
using Skyra.Core.Structures;
using Spectacles.NET.Broker.Amqp;
using Spectacles.NET.Rest;
using Spectacles.NET.Rest.Bucket;
using Spectacles.NET.Types;

namespace Skyra.Core
{
	public sealed class Client : IClient
	{
		internal Client(ClientOptions clientOptions)
		{
			var loader = new Loader();
			Cultures = loader.LoadCultures(new[] {"en-US", "es-ES", "sl"});
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

			ServiceProvider = new ServiceCollection()
				.AddSingleton(this)
				.BuildServiceProvider();

			Inhibitors = loader.LoadInhibitors(this);
			Events = loader.LoadEvents(this);
			Monitors = loader.LoadMonitors(this);
			Resolvers = loader.LoadResolvers(this);
			Commands = loader.LoadCommands(this);
		}

		public Dictionary<string, InhibitorInfo> Inhibitors { get; }
		public Dictionary<string, CommandInfo> Commands { get; }
		public Dictionary<string, EventInfo> Events { get; }
		public Dictionary<string, MonitorInfo> Monitors { get; }
		public Dictionary<Type, ResolverInfo> Resolvers { get; }
		public ServiceProvider ServiceProvider { get; }

		private string Token { get; }
		private string BrokerUri { get; }
		private string RedisUri { get; }
		private AmqpBroker Broker { get; }

		public ulong? Id { get; set; }
		public ulong[] Owners { get; set; }
		public RestClient Rest { get; private set; }
		public ImmutableDictionary<string, CultureInfo> Cultures { get; }
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
	}
}
