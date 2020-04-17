using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Events
{
	[Event]
	public class CommandRunEvent : StructureBase
	{
		public CommandRunEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandRunAsync += RunAsync;
		}

		private async Task RunAsync(Message message, string command, object?[] parameters)
		{
			await Task.FromResult(true);
		}
	}
}
