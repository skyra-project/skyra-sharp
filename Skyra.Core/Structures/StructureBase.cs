namespace Skyra.Core.Structures
{
	public abstract class StructureBase
	{
		protected StructureBase(IClient client)
		{
			Client = client;
		}

		protected IClient Client { get; }
	}
}
