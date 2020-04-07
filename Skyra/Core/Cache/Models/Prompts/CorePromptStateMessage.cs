using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CorePromptStateMessage : ICorePromptState
	{
		public CorePromptStateMessage(ulong authorId, ulong channelId, object context)
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

		[NotNull]
		public ICorePromptState Patch([NotNull] ICorePromptState value)
		{
			Context = value.Context;
			return this;
		}

		[NotNull]
		public string ToKey()
		{
			return ToKey(ChannelId, AuthorId);
		}

		public async Task RunAsync([NotNull] CoreMessage message, [NotNull] CorePromptStateMessage state)
		{
			await message.SendAsync(
				$"Oi there m8 you had a prompt set up, I replied to ya. By the way you once said `{state.Context}`");
		}

		[NotNull]
		public static string ToKey([NotNull] CoreMessage message)
		{
			return ToKey(message.ChannelId, message.AuthorId);
		}

		[NotNull]
		public static string ToKey(ulong channelId, ulong authorId)
		{
			return $"m:{channelId.ToString()}:{authorId.ToString()}";
		}
	}
}
