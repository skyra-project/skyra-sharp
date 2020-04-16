using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models.Prompts
{
	public class CorePromptStateReaction : ICorePromptState
	{
		public CorePromptStateReaction(ulong authorId, ulong messageId)
		{
			AuthorId = authorId;
			MessageId = messageId;
		}

		[JsonProperty("aid")]
		public ulong AuthorId { get; }

		[JsonProperty("mid")]
		public ulong MessageId { get; protected set; }

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
