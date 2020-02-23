namespace Skyra.Core.Structures
{
	public abstract class StructureBase
	{
		protected StructureBase(Client client)
		{
			Client = client;
		}

		protected Client Client { get; }
	}
}
