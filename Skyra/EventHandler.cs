using System;
using System.Text;
using Newtonsoft.Json;
using Skyra.Core;
using Skyra.Core.Models;
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

		public void HandleEvent(SkyraEvent @event, AmqpReceiveEventArgs args)
		{
			var data = Encoding.UTF8.GetString(args.Data);
			switch (@event)
			{
				case SkyraEvent.READY:
					OnReady(JsonConvert.DeserializeObject<ReadyDispatch>(data));
					break;
				case SkyraEvent.RESUMED:
					break;
				case SkyraEvent.CHANNEL_CREATE:
					break;
				case SkyraEvent.CHANNEL_UPDATE:
					break;
				case SkyraEvent.CHANNEL_DELETE:
					break;
				case SkyraEvent.CHANNEL_PINS_UPDATE:
					break;
				case SkyraEvent.GUILD_CREATE:
					OnGuildCreate(JsonConvert.DeserializeObject<Guild>(data));
					break;
				case SkyraEvent.GUILD_UPDATE:
					OnGuildUpdate(JsonConvert.DeserializeObject<Guild>(data));
					break;
				case SkyraEvent.GUILD_DELETE:
					break;
				case SkyraEvent.GUILD_BAN_ADD:
					OnGuildBanAdd(JsonConvert.DeserializeObject<GuildBanAddPayload>(data));
					break;
				case SkyraEvent.GUILD_BAN_REMOVE:
					OnGuildBanRemove(JsonConvert.DeserializeObject<GuildBanRemovePayload>(data));
					break;
				case SkyraEvent.GUILD_EMOJIS_UPDATE:
					break;
				case SkyraEvent.GUILD_INTEGRATIONS_UPDATE:
					break;
				case SkyraEvent.GUILD_MEMBER_ADD:
					break;
				case SkyraEvent.GUILD_MEMBER_REMOVE:
					break;
				case SkyraEvent.GUILD_MEMBER_UPDATE:
					break;
				case SkyraEvent.GUILD_MEMBERS_CHUNK:
					break;
				case SkyraEvent.GUILD_ROLE_CREATE:
					break;
				case SkyraEvent.GUILD_ROLE_UPDATE:
					break;
				case SkyraEvent.GUILD_ROLE_DELETE:
					break;
				case SkyraEvent.INVITE_CREATE:
					break;
				case SkyraEvent.INVITE_DELETE:
					break;
				case SkyraEvent.MESSAGE_CREATE:
					OnMessageCreate(JsonConvert.DeserializeObject<Message>(data));
					break;
				case SkyraEvent.MESSAGE_UPDATE:
					OnMessageUpdate(JsonConvert.DeserializeObject<MessageUpdatePayload>(data));
					break;
				case SkyraEvent.MESSAGE_DELETE:
					OnMessageDelete(JsonConvert.DeserializeObject<MessageDeletePayload>(data));
					break;
				case SkyraEvent.MESSAGE_DELETE_BULK:
					break;
				case SkyraEvent.MESSAGE_REACTION_ADD:
					break;
				case SkyraEvent.MESSAGE_REACTION_REMOVE:
					break;
				case SkyraEvent.MESSAGE_REACTION_REMOVE_ALL:
					break;
				case SkyraEvent.MESSAGE_REACTION_REMOVE_EMOJI:
					break;
				case SkyraEvent.PRESENCE_UPDATE:
					break;
				case SkyraEvent.PRESENCES_REPLACE:
					break;
				case SkyraEvent.TYPING_START:
					break;
				case SkyraEvent.USER_UPDATE:
					break;
				case SkyraEvent.VOICE_STATE_UPDATE:
					break;
				case SkyraEvent.VOICE_SERVER_UPDATE:
					break;
				case SkyraEvent.WEBHOOKS_UPDATE:
					break;
				case SkyraEvent.NOTIFY_TWITCH_STREAM_START:
					break;
				case SkyraEvent.NOTIFY_TWITCH_STREAM_END:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(@event), @event.ToString(), null);
			}
		}
	}
}
