using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class CommandArgumentExceptionEvent : StructureBase
	{
		public CommandArgumentExceptionEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandArgumentExceptionAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, ArgumentException exception)
		{
			await message.SendAsync(Client,
				$"Argument Error: {exception.InnerException?.Message ?? exception.Message}");
		}
	}
}
