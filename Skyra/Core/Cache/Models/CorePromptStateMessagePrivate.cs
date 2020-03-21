namespace Skyra.Core.Cache.Models
{
	public class CorePromptStateMessagePrivate : CorePromptStateMessagePublic
	{
		public CorePromptStateMessagePrivate(ulong channelId, ulong authorId) : base(channelId)
		{
			AuthorId = authorId;
			Type = CorePromptStateType.MessagePrivate;
		}

		public ulong AuthorId { get; }
	}
}