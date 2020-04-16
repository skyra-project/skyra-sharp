using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models.Prompts
{
	public class CorePromptStateReaction : ICorePromptState
	{
		public CorePromptStateReaction(ulong authorId, ulong messageId, object context)
		{
			AuthorId = authorId;
			MessageId = messageId;
			Context = context;
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("mid")]
		public ulong MessageId { get; protected set; }

		[JsonProperty("ctx")]
		public object Context { get; private set; }

		[NotNull]
		public ICorePromptState Patch([NotNull] ICorePromptState value)
		{
			Context = value.Context;
			return this;
		}

		[NotNull]
		public string ToKey()
		{
			return ToKey(MessageId, AuthorId);
		}

		public async Task RunAsync(CoreMessageReaction reaction, CorePromptStateReaction state)
		{
			await Task.CompletedTask;
		}

		[NotNull]
		public static string ToKey(ulong messageId, ulong authorId)
		{
			return $"r:{messageId.ToString()}:{authorId.ToString()}";
		}

		[NotNull]
		public static string ToKey([NotNull] CoreMessageReaction reaction)
		{
			return ToKey(reaction.MessageId, reaction.UserId);
		}
	}
}
