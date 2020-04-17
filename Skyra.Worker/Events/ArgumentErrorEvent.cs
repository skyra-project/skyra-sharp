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
	public class ArgumentErrorEvent : StructureBase
	{
		public ArgumentErrorEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnArgumentErrorAsync += RunAsync;
		}

		private async Task RunAsync([NotNull] Message message, string command, Exception exception)
		{
			Client.Logger.Error("[ARGUMENTS]: {Name} | {Exception}", command, exception);
			await message.SendAsync("Whoops! Something while processing arguments!");
		}
	}
}
