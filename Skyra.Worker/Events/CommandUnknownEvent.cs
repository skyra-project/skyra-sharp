using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Events
{
	[Event]
	public class CommandUnknownEvent : StructureBase
	{
		public CommandUnknownEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandUnknownAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command)
		{
			await Task.FromResult(true);
		}
	}
}
