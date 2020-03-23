using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CorePromptStateMessage : ICorePromptState
	{
		private CorePromptStateMessage(ulong authorId, ulong channelId, object context)
		{
			AuthorId = authorId;
			ChannelId = channelId;
			Context = context;
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; }

		[JsonProperty("ctx")]
		public object Context { get; private set; }

		public ICorePromptState Patch(ICorePromptState value)
		{
			Context = value.Context;
			return this;
		}

		public string ToKey()
		{
			return $"m:{ChannelId.ToString()}:{AuthorId.ToString()}";
		}

		public static string ToKey(CoreMessage message)
		{
			return ToKey(message.ChannelId, message.AuthorId);
		}

		public static string ToKey(ulong channelId, ulong authorId)
		{
			return $"m:{channelId.ToString()}:{authorId.ToString()}";
		}
	}
}
