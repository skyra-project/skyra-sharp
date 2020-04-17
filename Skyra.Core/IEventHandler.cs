using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Models;
using Skyra.Core.Structures.Exceptions;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;
using Guild = Spectacles.NET.Types.Guild;
using Message = Skyra.Core.Cache.Models.Message;

namespace Skyra.Core
{
	public interface IEventHandler
	{
		Func<Message, string, Exception, Task> OnArgumentErrorAsync { get; set; }
		Func<Message, string, ArgumentException, Task> OnCommandArgumentExceptionAsync { get; set; }
		Func<Message, string, object?[], Exception, Task> OnCommandErrorAsync { get; set; }
		Func<Message, string, InhibitorException, Task> OnCommandInhibitedAsync { get; set; }
		Func<Message, string, object?[], Task> OnCommandRunAsync { get; set; }
		Func<Message, string, object?[], Task> OnCommandSuccessAsync { get; set; }
		Func<Message, string, Task> OnCommandUnknownAsync { get; set; }
		Func<Message, string, Exception, Task> OnInhibitorExceptionAsync { get; set; }
		Func<Message, Task> OnMessageCreateAsync { get; set; }
		Func<MessageDeletePayload, Message?, Task> OnMessageDeleteAsync { get; set; }
		Func<Message?, Message, Task> OnMessageUpdateAsync { get; set; }
		Func<Spectacles.NET.Types.Message, Task> OnRawMessageCreateAsync { get; set; }
		Func<MessageDeletePayload, Task> OnRawMessageDeleteAsync { get; set; }
		Func<PromptDataMessage, Message, Task> OnRawMessagePromptAsync { get; set; }
		Func<MessageUpdatePayload, Task> OnRawMessageUpdateAsync { get; set; }
		Func<PromptDataReaction, MessageReaction, Task> OnRawReactionPromptAsync { get; set; }
		Func<MessageReactionAddPayload, Task> OnRawMessageReactionAddAsync { get; set; }
		Func<MessageReactionRemovePayload, Task> OnRawMessageReactionRemoveAsync { get; set; }
		Func<MessageReactionRemoveAllPayload, Task> OnRawMessageReactionRemoveAllAsync { get; set; }
		Func<MessageReactionRemoveEmojiPayload, Task> OnRawMessageReactionRemoveEmojiAsync { get; set; }
		event Action<ReadyDispatch> OnReady;
		event Action<Guild> OnRawGuildCreate;
		event Action<Guild> OnRawGuildUpdate;
		event Action<UnavailableGuild> OnRawGuildDelete;
		event Action<GuildBanAddPayload> OnRawGuildBanAdd;
		event Action<GuildBanRemovePayload> OnRawGuildBanRemove;
		void HandleEvent(SkyraEvent @event, [NotNull] AmqpReceiveEventArgs args);
	}
}
