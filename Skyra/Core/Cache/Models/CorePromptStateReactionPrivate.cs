namespace Skyra.Core.Cache.Models
{
	public class CorePromptStateReactionPrivate : CorePromptStateReactionPublic
	{
		public CorePromptStateReactionPrivate(ulong messageId, ulong authorId) : base(messageId)
		{
			AuthorId = authorId;
			Type = CorePromptStateType.ReactionPrivate;
		}

		public ulong AuthorId { get; }
	}
}