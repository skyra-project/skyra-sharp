using Skyra.Structures.Base;

namespace Skyra.Structures
{
	public abstract class Event : Piece
	{
		public readonly Client Client;
		public readonly EventHandler EventHandler;

		protected Event(Client client, EventOptions options)
		{
			Client = client;
			EventHandler = client.EventHandler;
			Name = options.Name;
		}
	}
}
