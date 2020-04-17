using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models.Prompts
{
	public interface IPromptDataReaction
	{
		ulong AuthorId { get; }
		ulong MessageId { get; }
		string ToKey();
		Task<TimeSpan?> RunAsync(MessageReactionAddPayload reaction);

		[NotNull]
		static string ToKey(ulong messageId, ulong authorId)
		{
			return $"r:{messageId.ToString()}:{authorId.ToString()}";
		}

		[NotNull]
		static string ToKey([NotNull] MessageReaction reaction)
		{
			return ToKey(reaction.MessageId, reaction.UserId);
		}

		[NotNull]
		static string ToKey([NotNull] MessageReactionAddPayload payload)
		{
			return $"r:{payload.MessageId}:{payload.UserId}";
		}
	}
}
