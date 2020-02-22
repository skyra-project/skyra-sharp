using Skyra.Core.Structures.Base;

namespace Skyra.Core.Structures
{
	public abstract class Event : Piece
	{
		protected Event(Client client, EventOptions options) : base(options.Name)
		{
			Client = client;
			EventHandler = client.EventHandler;
		}

		public Client Client { get; }
		public EventHandler EventHandler { get; }
	}
}
