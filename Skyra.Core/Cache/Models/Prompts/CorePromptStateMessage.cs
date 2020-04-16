using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CorePromptStateMessage : ICorePromptState
	{
		public CorePromptStateMessage(ulong authorId, ulong channelId)
		{
			AuthorId = authorId;
			ChannelId = channelId;
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; }

		[NotNull]
		public string ToKey()
		{
			return ToKey(ChannelId, AuthorId);
		}

		public async Task<TimeSpan?> RunAsync([NotNull] CoreMessage message, [NotNull] CorePromptStateMessage state)
		{
			await Task.CompletedTask;
			return null;
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
