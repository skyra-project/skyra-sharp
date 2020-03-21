namespace Skyra.Core.Cache.Models
{
	public class CorePromptStateReactionPublic : ICorePromptState
	{
		public CorePromptStateReactionPublic(ulong messageId)
		{
			MessageId = messageId;
			Type = CorePromptStateType.ReactionPublic;
		}

		public ulong MessageId { get; }
		public CorePromptStateType Type { get; protected set; }

		public ICorePromptState Patch(ICorePromptState value)
		{
			return this;
		}
	}
}