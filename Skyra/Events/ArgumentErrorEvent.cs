using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class ArgumentErrorEvent : StructureBase
	{
		public ArgumentErrorEvent(Client client) : base(client)
		{
			Client.EventHandler.OnArgumentErrorAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, Exception exception)
		{
			Client.Logger.Error("[ARGUMENTS]: {Name} | {Exception}", command, exception);
			await message.SendAsync(Client, "Whoops! Something while processing arguments!");
		}
	}
}
