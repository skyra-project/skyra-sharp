using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Exceptions;

namespace Skyra.Worker.Events
{
	[Event]
	public class CommandInhibitedEvent : StructureBase
	{
		public CommandInhibitedEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandInhibitedAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, [NotNull] InhibitorException exception)
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
