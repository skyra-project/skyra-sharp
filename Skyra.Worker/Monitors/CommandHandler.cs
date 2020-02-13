using System.Threading.Tasks;
using Skyra.Framework;
using Skyra.Framework.Structures;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	public class CommandHandler : Monitor
	{
		public CommandHandler(Client client) : base(client,
			new MonitorOptions {Name = "CommandHandler", IgnoreOthers = false, IgnoreEdits = false})
		{
		}

		public override Task<bool> Run(Message message)
		{
			return Task.FromResult(true);
		}
	}
}
