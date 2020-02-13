using System.Linq;
using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventMessage : Event
	{
		public EventMessage(Client client) : base(client, new EventOptions(nameof(EventMessage)))
		{
			EventHandler.OnMessageCreate += Run;
		}

		private void Run(Message message)
		{
			Task.Run(async () =>
			{
				foreach (var monitor in Client.Monitors.Values.Where(monitor => monitor.ShouldRun(message)))
				{
					if (!await monitor.Run(message)) break;
				}
			});
		}
	}
}
