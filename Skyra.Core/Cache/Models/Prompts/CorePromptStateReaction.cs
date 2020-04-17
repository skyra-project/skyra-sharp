using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CorePromptStateReaction : ICorePromptState, ICorePromptStateReaction
	{
		public CorePromptStateReaction(ulong authorId, ulong messageId)
		{
			AuthorId = authorId;
			MessageId = messageId;
		}

		[NotNull]
		public string ToKey()
		{
			return ICorePromptStateReaction.ToKey(MessageId, AuthorId);
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("mid")]
		public ulong MessageId { get; set; }

		public async Task<TimeSpan?> RunAsync(MessageReactionAddPayload reaction)
		{
			await Task.CompletedTask;
			return null;
		}
	}
}
