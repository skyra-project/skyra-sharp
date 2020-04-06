using System.Threading.Tasks;
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

		public ICorePromptState Patch(ICorePromptState value)
		{
			Context = value.Context;
			return this;
		}

		public string ToKey()
		{
			return ToKey(ChannelId, AuthorId);
		}

		public async Task RunAsync(CoreMessage message, CorePromptStateMessage state)
		{
			await message.SendAsync(
				$"Oi there m8 you had a prompt set up, I replied to ya. By the way you once said `{state.Context}`");
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
