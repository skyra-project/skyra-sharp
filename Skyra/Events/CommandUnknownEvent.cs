using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class CommandUnknownEvent : StructureBase
	{
		public CommandUnknownEvent(Client client) : base(client)
		{
			Client.EventHandler.OnCommandUnknownAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command)
		{
			await Task.FromResult(true);
		}
	}
}
