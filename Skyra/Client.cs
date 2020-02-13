using System;
using System.Threading.Tasks;
using Skyra.Cache;
using Skyra.Structures;
using Skyra.Structures.Base;
using Spectacles.NET.Broker.Amqp;
using Spectacles.NET.Types;

namespace Skyra
{
	public class Client
	{
		private readonly Uri _brokerUri;
		private readonly AmqpBroker _broker;

		public EventHandler EventHandler { get; }
		public CacheClient Cache { get; }

		public readonly Store<Event> Events = new Store<Event>();
		public readonly Store<Monitor> Monitors = new Store<Monitor>();

		public Client(string brokerName, Uri brokerUri)
		{
			EventHandler = new EventHandler(this);
			Cache = new CacheClient(Environment.GetEnvironmentVariable("REDIS_PREFIX") ?? "skyra");

			_brokerUri = brokerUri;
			_broker = new AmqpBroker(brokerName);
			_broker.Receive += (sender, args) =>
			{
				EventHandler.HandleEvent((GatewayEvent) Enum.Parse(typeof(GatewayEvent), args.Event), args);
				_broker.Ack(args.Event, args.DeliveryTag);
			};
		}

		public async Task ConnectAsync()
		{
			await _broker.ConnectAsync(_brokerUri);
			await _broker.SubscribeAsync(new[]
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
