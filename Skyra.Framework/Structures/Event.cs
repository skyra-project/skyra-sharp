using Skyra.Framework.Structures.Base;

namespace Skyra.Framework.Structures
{
	public abstract class Event : Piece
	{
		public readonly Client Client;

		protected Event(Client client, EventOptions options)
		{
			Client = client;
			Name = options.Name;
		}
	}

	public struct EventOptions
	{
		public string Name;
	}
}
