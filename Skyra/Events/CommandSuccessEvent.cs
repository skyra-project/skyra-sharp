using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class CommandSuccessEvent : StructureBase
	{
		public CommandSuccessEvent(Client client) : base(client)
		{
			Client.EventHandler.OnCommandSuccessAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, object?[] parameters)
		{
			await Task.FromResult(true);
		}
	}
}
