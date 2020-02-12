using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Framework.Models.Gateway;
using Spectacles.NET.Broker.Amqp;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;

namespace Skyra.Framework
{
	public class Client
	{
		private readonly Uri _brokerUri;
		private readonly AmqpBroker _broker;

		public event EventHandler<OnReadyArgs> OnReady;
		public event EventHandler<OnMessageCreateArgs> OnMessageCreate;

		public Client(string brokerName, Uri brokerUri)
		{
			_brokerUri = brokerUri;
			_broker = new AmqpBroker(brokerName);
			_broker.Receive += (sender, args) =>
			{
				HandleEvent((GatewayEvent) Enum.Parse(typeof(GatewayEvent), args.Event), args);
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
				"PRESENCE_UPDATE",
				"TYPING_START",
				"USER_UPDATE",
				"VOICE_STATE_UPDATE",
				"VOICE_SERVER_UPDATE",
				"WEBHOOKS_UPDATE"
			});
		}

		private void HandleEvent(GatewayEvent @event, AmqpReceiveEventArgs args)
		{
			var data = Encoding.UTF8.GetString(args.Data);
			switch (@event)
			{
				case GatewayEvent.READY:
					OnReady?.Invoke(this, new OnReadyArgs(JsonConvert.DeserializeObject<ReadyDispatch>(data)));
					break;
				case GatewayEvent.RESUMED:
					break;
				case GatewayEvent.CHANNEL_CREATE:
					break;
				case GatewayEvent.CHANNEL_UPDATE:
					break;
				case GatewayEvent.CHANNEL_DELETE:
					break;
				case GatewayEvent.CHANNEL_PINS_UPDATE:
					break;
				case GatewayEvent.GUILD_CREATE:
					break;
				case GatewayEvent.GUILD_UPDATE:
					break;
				case GatewayEvent.GUILD_DELETE:
					break;
				case GatewayEvent.GUILD_BAN_ADD:
					break;
				case GatewayEvent.GUILD_BAN_REMOVE:
					break;
				case GatewayEvent.GUILD_EMOJIS_UPDATE:
					break;
				case GatewayEvent.GUILD_INTEGRATIONS_UPDATE:
					break;
				case GatewayEvent.GUILD_MEMBER_ADD:
					break;
				case GatewayEvent.GUILD_MEMBER_REMOVE:
					break;
				case GatewayEvent.GUILD_MEMBER_UPDATE:
					break;
				case GatewayEvent.GUILD_MEMBERS_CHUNK:
					break;
				case GatewayEvent.GUILD_ROLE_CREATE:
					break;
				case GatewayEvent.GUILD_ROLE_UPDATE:
					break;
				case GatewayEvent.GUILD_ROLE_DELETE:
					break;
				case GatewayEvent.INVITE_CREATE:
					break;
				case GatewayEvent.INVITE_DELETE:
					break;
				case GatewayEvent.MESSAGE_CREATE:
					OnMessageCreate?.Invoke(this, new OnMessageCreateArgs(JsonConvert.DeserializeObject<Message>(data)));
					break;
				case GatewayEvent.MESSAGE_UPDATE:
					break;
				case GatewayEvent.MESSAGE_DELETE:
					break;
				case GatewayEvent.MESSAGE_DELETE_BULK:
					break;
				case GatewayEvent.MESSAGE_REACTION_ADD:
					break;
				case GatewayEvent.MESSAGE_REACTION_REMOVE:
					break;
				case GatewayEvent.MESSAGE_REACTION_REMOVE_ALL:
					break;
				case GatewayEvent.MESSAGE_REACTION_REMOVE_EMOJI:
					break;
				case GatewayEvent.PRESENCE_UPDATE:
					break;
				case GatewayEvent.PRESENCES_REPLACE:
					break;
				case GatewayEvent.TYPING_START:
					break;
				case GatewayEvent.USER_UPDATE:
					break;
				case GatewayEvent.VOICE_STATE_UPDATE:
					break;
				case GatewayEvent.VOICE_SERVER_UPDATE:
					break;
				case GatewayEvent.WEBHOOKS_UPDATE:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
			}
		}
	}
}
