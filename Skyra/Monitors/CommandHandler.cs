using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	public class CommandHandler : Monitor
	{
		public CommandHandler(Client client) : base(client,
			new MonitorOptions(nameof(CommandHandler), ignoreOthers: false, ignoreSelf: false))
		{
		}

		public override Task<bool> Run(Message message)
		{
			return Task.FromResult(true);
		}
	}
}
