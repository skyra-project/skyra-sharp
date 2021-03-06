using System;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Models;
using Skyra.Core.Structures.Exceptions;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;
using Guild = Spectacles.NET.Types.Guild;
using Message = Skyra.Core.Cache.Models.Message;

// ReSharper disable RedundantDefaultMemberInitializer

namespace Skyra.Worker
{
	public sealed class EventHandler : IEventHandler
	{
		public Func<Message, string, Exception, Task> OnArgumentErrorAsync { get; set; } = default!;

		public Func<Message, string, ArgumentException, Task> OnCommandArgumentExceptionAsync { get; set; } =
			default!;

		public Func<Message, string, object?[], Exception, Task> OnCommandErrorAsync { get; set; } = default!;
		public Func<Message, string, InhibitorException, Task> OnCommandInhibitedAsync { get; set; } = default!;
		public Func<Message, string, object?[], Task> OnCommandRunAsync { get; set; } = default!;
		public Func<Message, string, object?[], Task> OnCommandSuccessAsync { get; set; } = default!;
		public Func<Message, string, Task> OnCommandUnknownAsync { get; set; } = default!;
		public Func<Message, string, Exception, Task> OnInhibitorExceptionAsync { get; set; } = default!;
		public Func<Message, Task> OnMessageCreateAsync { get; set; } = default!;
		public Func<MessageDeletePayload, Message?, Task> OnMessageDeleteAsync { get; set; } = default!;
		public Func<Message?, Message, Task> OnMessageUpdateAsync { get; set; } = default!;
		public Func<Spectacles.NET.Types.Message, Task> OnRawMessageCreateAsync { get; set; } = default!;
		public Func<MessageDeletePayload, Task> OnRawMessageDeleteAsync { get; set; } = default!;
		public Func<PromptDataMessage, Message, Task> OnRawMessagePromptAsync { get; set; } = default!;
		public Func<MessageUpdatePayload, Task> OnRawMessageUpdateAsync { get; set; } = default!;
		public Func<MessageReactionAddPayload, Task> OnRawMessageReactionAddAsync { get; set; } = default!;
		public Func<MessageReactionRemovePayload, Task> OnRawMessageReactionRemoveAsync { get; set; } = default!;
		public Func<MessageReactionRemoveAllPayload, Task> OnRawMessageReactionRemoveAllAsync { get; set; } = default!;

		public Func<MessageReactionRemoveEmojiPayload, Task> OnRawMessageReactionRemoveEmojiAsync { get; set; } =
			default!;

		public Func<PromptDataReaction, MessageReaction, Task> OnRawReactionPromptAsync { get; set; } =
			default!;

		public event Action<ReadyDispatch> OnReady = dispatch => { };
		public event Action<Guild> OnRawGuildCreate = dispatch => { };
		public event Action<Guild> OnRawGuildUpdate = dispatch => { };
		public event Action<UnavailableGuild> OnRawGuildDelete = dispatch => { };
		public event Action<GuildBanAddPayload> OnRawGuildBanAdd = dispatch => { };
		public event Action<GuildBanRemovePayload> OnRawGuildBanRemove = dispatch => { };

		public void HandleEvent(SkyraEvent @event, [NotNull] AmqpReceiveEventArgs args)
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
					OnRawMessageCreateAsync(JsonConvert.DeserializeObject<Spectacles.NET.Types.Message>(data));
					break;
				case SkyraEvent.MESSAGE_UPDATE:
					OnRawMessageUpdateAsync(JsonConvert.DeserializeObject<MessageUpdatePayload>(data));
					break;
				case SkyraEvent.MESSAGE_DELETE:
					OnRawMessageDeleteAsync(JsonConvert.DeserializeObject<MessageDeletePayload>(data));
					break;
				case SkyraEvent.MESSAGE_DELETE_BULK:
					break;
				case SkyraEvent.MESSAGE_REACTION_ADD:
					OnRawMessageReactionAddAsync(JsonConvert.DeserializeObject<MessageReactionAddPayload>(data));
					break;
				case SkyraEvent.MESSAGE_REACTION_REMOVE:
					OnRawMessageReactionRemoveAsync(JsonConvert.DeserializeObject<MessageReactionRemovePayload>(data));
					break;
				case SkyraEvent.MESSAGE_REACTION_REMOVE_ALL:
					OnRawMessageReactionRemoveAllAsync(
						JsonConvert.DeserializeObject<MessageReactionRemoveAllPayload>(data));
					break;
				case SkyraEvent.MESSAGE_REACTION_REMOVE_EMOJI:
					OnRawMessageReactionRemoveEmojiAsync(
						JsonConvert.DeserializeObject<MessageReactionRemoveEmojiPayload>(data));
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
