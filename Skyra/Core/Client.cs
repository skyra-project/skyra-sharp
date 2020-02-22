using System;
using System.Threading.Tasks;
using Skyra.Core.Cache;
using Skyra.Core.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Base;
using Spectacles.NET.Broker.Amqp;
using Spectacles.NET.Rest;
using Spectacles.NET.Rest.Bucket;
using Spectacles.NET.Types;

namespace Skyra.Core
{
	public class Client
	{
		public Client(ClientOptions clientOptions)
		{
			EventHandler = new EventHandler(this);
			Cache = new CacheClient(clientOptions.RedisPrefix);

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

		public Store<Event> Events { get; } = new Store<Event>();
		public MonitorStore Monitors { get; } = new MonitorStore();

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
	}
}
