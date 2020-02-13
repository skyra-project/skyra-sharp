using Skyra.Structures.Base;

namespace Skyra.Structures
{
	public abstract class Event : Piece
	{
		public Client Client { get; }
		public EventHandler EventHandler { get; }

		protected Event(Client client, EventOptions options) : base(options.Name)
		{
			Client = client;
			EventHandler = client.EventHandler;
		}
	}
}
