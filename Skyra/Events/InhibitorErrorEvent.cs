using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class InhibitorErrorEvent : StructureBase
	{
		public InhibitorErrorEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnInhibitorExceptionAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message, string command, Exception exception)
		{
			Client.Logger.Error("[INHIBITORS]: {Name} | {Exception}", command, exception);
			await message.SendAsync(Client,
				$"Inhibitor Error: {exception.InnerException?.Message ?? exception.Message}");
		}
	}
}
