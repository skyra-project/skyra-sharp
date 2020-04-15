using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Models;
using Skyra.Core.Structures.Exceptions;
using Spectacles.NET.Broker.Amqp.EventArgs;
using Spectacles.NET.Types;

namespace Skyra.Core
{
	public interface IEventHandler
	{
		Func<CoreMessage, string, Exception, Task> OnArgumentErrorAsync { get; set; }
		Func<CoreMessage, string, ArgumentException, Task> OnCommandArgumentExceptionAsync { get; set; }
		Func<CoreMessage, string, object?[], Exception, Task> OnCommandErrorAsync { get; set; }
		Func<CoreMessage, string, InhibitorException, Task> OnCommandInhibitedAsync { get; set; }
		Func<CoreMessage, string, object?[], Task> OnCommandRunAsync { get; set; }
		Func<CoreMessage, string, object?[], Task> OnCommandSuccessAsync { get; set; }
		Func<CoreMessage, string, Task> OnCommandUnknownAsync { get; set; }
		Func<CoreMessage, string, Exception, Task> OnInhibitorExceptionAsync { get; set; }
		Func<CoreMessage, Task> OnMessageCreateAsync { get; set; }
		Func<MessageDeletePayload, CoreMessage?, Task> OnMessageDeleteAsync { get; set; }
		Func<CoreMessage?, CoreMessage, Task> OnMessageUpdateAsync { get; set; }
		Func<Message, Task> OnRawMessageCreateAsync { get; set; }
		Func<MessageDeletePayload, Task> OnRawMessageDeleteAsync { get; set; }
		Func<CorePromptStateMessage, CoreMessage, Task> OnRawMessagePromptAsync { get; set; }
		Func<MessageUpdatePayload, Task> OnRawMessageUpdateAsync { get; set; }
		Func<CorePromptStateReaction, CoreMessageReaction, Task> OnRawReactionPromptAsync { get; set; }
		event Action<ReadyDispatch> OnReady;
		event Action<Guild> OnRawGuildCreate;
		event Action<Guild> OnRawGuildUpdate;
		event Action<UnavailableGuild> OnRawGuildDelete;
		event Action<GuildBanAddPayload> OnRawGuildBanAdd;
		event Action<GuildBanRemovePayload> OnRawGuildBanRemove;
		void HandleEvent(SkyraEvent @event, [NotNull] AmqpReceiveEventArgs args);
	}
}
