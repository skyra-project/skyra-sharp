using System.Linq;
using System.Threading.Tasks;
using Skyra.Models.Gateway;
using Skyra.Structures;

namespace Skyra.Events
{
	public class EventMessage : Event
	{
		public EventMessage(Client client) : base(client, new EventOptions(nameof(EventMessage)))
		{
			EventHandler.OnMessageCreate += Run;
		}

		private void Run(OnMessageCreateArgs args)
		{
			Task.Run(async () =>
			{
				foreach (var monitor in Client.Monitors.Values.Where(monitor => monitor.ShouldRun(args.Data)))
				{
					if (!await monitor.Run(args.Data)) break;
				}
			});
		}
	}
}
