using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class CommandErrorEvent : StructureBase
	{
		public CommandErrorEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnCommandErrorAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, object?[] parameters, Exception exception)
		{
			Client.Logger.Error("[COMMANDS]: {Name} | {Exception}", command, exception);
			await message.SendAsync("Whoops! Something happened while processing the command!");
		}
	}
}
