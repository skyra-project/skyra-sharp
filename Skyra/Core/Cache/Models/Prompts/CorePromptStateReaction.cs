using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CorePromptStateReaction : ICorePromptState
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
		public ulong MessageId { get; }

		[JsonProperty("ctx")]
		public object Context { get; private set; }

		public ICorePromptState Patch(ICorePromptState value)
		{
			Context = value.Context;
			return this;
		}

		public string ToKey()
		{
			return $"r:{MessageId.ToString()}:{AuthorId.ToString()}";
		}

		public static string ToKey(CoreMessageReaction reaction)
		{
			return $"r:{reaction.MessageId.ToString()}:{reaction.UserId.ToString()}";
		}
	}
}
