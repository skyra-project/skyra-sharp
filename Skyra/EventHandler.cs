using System;
using System.Text;
using Newtonsoft.Json;
using Skyra.Core;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;

namespace Skyra
{
	public class EventHandler
	{
		public EventHandler(Client client)
		{
			Client = client;
		}

		private Client Client { get; }
		public event Action<ReadyDispatch> OnReady = dispatch => { };
		public event Action<Guild> OnGuildCreate = dispatch => { };
		public event Action<Guild> OnGuildUpdate = dispatch => { };
		public event Action<GuildBanAddPayload> OnGuildBanAdd = dispatch => { };
		public event Action<GuildBanRemovePayload> OnGuildBanRemove = dispatch => { };
		public event Action<Message> OnMessageCreate = dispatch => { };
		public event Action<MessageUpdatePayload> OnMessageUpdate = dispatch => { };
		public event Action<MessageDeletePayload> OnMessageDelete = dispatch => { };

		public void HandleEvent(GatewayEvent @event, AmqpReceiveEventArgs args)
		{
			var data = Encoding.UTF8.GetString(args.Data);
			switch (@event)
			{
				case GatewayEvent.READY:
					OnReady(JsonConvert.DeserializeObject<ReadyDispatch>(data));
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
					OnGuildCreate(JsonConvert.DeserializeObject<Guild>(data));
					break;
				case GatewayEvent.GUILD_UPDATE:
					OnGuildUpdate(JsonConvert.DeserializeObject<Guild>(data));
					break;
				case GatewayEvent.GUILD_DELETE:
					break;
				case GatewayEvent.GUILD_BAN_ADD:
					OnGuildBanAdd(JsonConvert.DeserializeObject<GuildBanAddPayload>(data));
					break;
				case GatewayEvent.GUILD_BAN_REMOVE:
					OnGuildBanRemove(JsonConvert.DeserializeObject<GuildBanRemovePayload>(data));
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
					OnMessageCreate(JsonConvert.DeserializeObject<Message>(data));
					break;
				case GatewayEvent.MESSAGE_UPDATE:
					OnMessageUpdate(JsonConvert.DeserializeObject<MessageUpdatePayload>(data));
					break;
				case GatewayEvent.MESSAGE_DELETE:
					OnMessageDelete(JsonConvert.DeserializeObject<MessageDeletePayload>(data));
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
					throw new ArgumentOutOfRangeException(nameof(@event), @event.ToString(), null);
			}
		}
	}
}
