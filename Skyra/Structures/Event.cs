using Skyra.Structures.Base;

namespace Skyra.Structures
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
}
