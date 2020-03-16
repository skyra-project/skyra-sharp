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
		public CommandInhibitedEvent(Client client) : base(client)
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
				await message.SendAsync(Client, $"Inhibitor Error: {exception.Message}");
			}
		}
	}
}
