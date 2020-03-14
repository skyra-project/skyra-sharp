using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Core.Cache.Models;
using Skyra.Core.Models;
using Skyra.Core.Structures.Exceptions;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;

namespace Skyra
{
	public sealed class EventHandler
	{
		internal Func<CoreMessage, string, Exception, Task> OnArgumentErrorAsync = default!;
		internal Func<CoreMessage, string, ArgumentException, Task> OnCommandArgumentExceptionAsync = default!;
		internal Func<CoreMessage, string, object?[], Exception, Task> OnCommandErrorAsync = default!;
		internal Func<CoreMessage, string, InhibitorException, Task> OnCommandInhibitedAsync = default!;
		internal Func<CoreMessage, string, object?[], Task> OnCommandRunAsync = default!;
		internal Func<CoreMessage, string, object?[], Task> OnCommandSuccessAsync = default!;
		internal Func<CoreMessage, string, Task> OnCommandUnknownAsync = default!;
		internal Func<CoreMessage, string, Exception, Task> OnInhibitorExceptionAsync = default!;
		internal Action<CoreMessage> OnMessageCreate = default!;
		internal Action<MessageDeletePayload, CoreMessage?> OnMessageDelete = default!;
		internal Action<CoreMessage?, CoreMessage> OnMessageUpdate = default!;

		public event Action<ReadyDispatch> OnReady = dispatch => { };
		public event Action<Guild> OnRawGuildCreate = dispatch => { };
		public event Action<Guild> OnRawGuildUpdate = dispatch => { };
		public event Action<UnavailableGuild> OnRawGuildDelete = dispatch => { };
		public event Action<GuildBanAddPayload> OnRawGuildBanAdd = dispatch => { };
		public event Action<GuildBanRemovePayload> OnRawGuildBanRemove = dispatch => { };
		public event Action<Message> OnRawMessageCreate = dispatch => { };
		public event Action<MessageUpdatePayload> OnRawMessageUpdate = dispatch => { };
		public event Action<MessageDeletePayload> OnRawMessageDelete = dispatch => { };

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
					OnRawGuildCreate(JsonConvert.DeserializeObject<Guild>(data));
					break;
				case SkyraEvent.GUILD_UPDATE:
					OnRawGuildUpdate(JsonConvert.DeserializeObject<Guild>(data));
					break;
				case SkyraEvent.GUILD_DELETE:
					OnRawGuildDelete(JsonConvert.DeserializeObject<UnavailableGuild>(data));
					break;
				case SkyraEvent.GUILD_BAN_ADD:
					OnRawGuildBanAdd(JsonConvert.DeserializeObject<GuildBanAddPayload>(data));
					break;
				case SkyraEvent.GUILD_BAN_REMOVE:
					OnRawGuildBanRemove(JsonConvert.DeserializeObject<GuildBanRemovePayload>(data));
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
					OnRawMessageCreate(JsonConvert.DeserializeObject<Message>(data));
					break;
				case SkyraEvent.MESSAGE_UPDATE:
					OnRawMessageUpdate(JsonConvert.DeserializeObject<MessageUpdatePayload>(data));
					break;
				case SkyraEvent.MESSAGE_DELETE:
					OnRawMessageDelete(JsonConvert.DeserializeObject<MessageDeletePayload>(data));
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
