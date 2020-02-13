using System;
using System.Text;
using Newtonsoft.Json;
using Skyra.Models.Gateway;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;

namespace Skyra
{
	public class EventHandler
	{
		public event Action<OnReadyArgs> OnReady;
		public event Action<OnMessageCreateArgs> OnMessageCreate;

		private readonly Client Client;

		public EventHandler(Client client)
		{
			Client = client;
		}

		public void HandleEvent(GatewayEvent @event, AmqpReceiveEventArgs args)
		{
			var data = Encoding.UTF8.GetString(args.Data);
			switch (@event)
			{
				case GatewayEvent.READY:
					OnReady(new OnReadyArgs(JsonConvert.DeserializeObject<ReadyDispatch>(data)));
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
					OnMessageCreate(new OnMessageCreateArgs(JsonConvert.DeserializeObject<Message>(data)));
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
