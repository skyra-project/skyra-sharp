using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Events
{
	[Event]
	public class CommandArgumentExceptionEvent : StructureBase
	{
		public CommandArgumentExceptionEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandArgumentExceptionAsync += RunAsync;
		}

		private async Task RunAsync([NotNull] Message message, string command,
			[NotNull] ArgumentException exception)
		{
			await message.SendAsync($"Argument Error: {exception.InnerException?.Message ?? exception.Message}");
		}
	}
}
