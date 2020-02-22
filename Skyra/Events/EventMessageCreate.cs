using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventMessageCreate : Event
	{
		public EventMessageCreate(Client client) : base(client, new EventOptions(nameof(EventMessageCreate)))
		{
			EventHandler.OnMessageCreate += Run;
		}

		private void Run(Message message)
		{
			Task.Run(() => Task.WhenAll(Client.Cache.Messages.SetAsync(message), Client.Monitors.Run(message)));
		}
	}
}
