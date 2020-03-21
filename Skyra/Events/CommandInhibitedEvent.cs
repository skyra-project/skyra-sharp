using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Exceptions;

namespace Skyra.Events
{
	[Event]
	public class CommandInhibitedEvent : StructureBase
	{
		public CommandInhibitedEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandInhibitedAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, InhibitorException exception)
		{
			if (exception.Silent)
			{
				await Task.FromResult(true);
			}
			else
			{
				await message.SendAsync($"Inhibitor Error: {exception.Message}");
			}
		}
	}
}
