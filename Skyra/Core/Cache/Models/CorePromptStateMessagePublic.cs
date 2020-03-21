namespace Skyra.Core.Cache.Models
{
	public class CorePromptStateMessagePublic : ICorePromptState
	{
		public CorePromptStateMessagePublic(ulong channelId)
		{
			ChannelId = channelId;
			Type = CorePromptStateType.MessagePublic;
		}

		public ulong ChannelId { get; }
		public CorePromptStateType Type { get; protected set; }

		public ICorePromptState Patch(ICorePromptState value)
		{
			return this;
		}
	}
}