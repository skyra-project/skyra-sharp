namespace Skyra.Core.Cache.Models
{
	public interface ICorePromptState
	{
		public CorePromptStateType Type { get; }
		public ICorePromptState Patch(ICorePromptState value);
	}
}