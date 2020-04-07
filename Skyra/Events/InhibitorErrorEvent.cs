using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
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

		private async Task RunAsync([NotNull] CoreMessage message, string command, [NotNull] Exception exception)
		{
			Client.Logger.Error("[INHIBITORS]: {Name} | {Exception}", command, exception);
			await message.SendAsync($"Inhibitor Error: {exception.InnerException?.Message ?? exception.Message}");
		}
	}
}
