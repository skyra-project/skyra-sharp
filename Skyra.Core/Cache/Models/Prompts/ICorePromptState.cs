namespace Skyra.Core.Cache.Models.Prompts
{
	public interface ICorePromptState
	{
		public object Context { get; }
		public ICorePromptState Patch(ICorePromptState value);
		public string ToKey();
	}
}
