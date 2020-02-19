using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	public class CommandHandler : Monitor
	{
		private Commands.CommandHandler _handler = new Commands.CommandHandler();
		public CommandHandler(Client client) : base(client,
			new MonitorOptions(nameof(CommandHandler), ignoreOthers: false, ignoreSelf: false))
		{
			_handler.Load(client);
		}

		public override Task<bool> Run(Message message)
		{
			_handler.Run(message);
			return Task.FromResult(true);
		}
	}
}
