using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	public class CommandHandler : Monitor
	{
		private readonly Core.Structures.CommandHandler _handler = new Core.Structures.CommandHandler();

		public CommandHandler(Client client) : base(client,
			new MonitorOptions(nameof(CommandHandler), ignoreOthers: false, ignoreEdits: false))
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
