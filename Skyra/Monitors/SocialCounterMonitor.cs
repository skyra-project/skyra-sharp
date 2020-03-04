using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Monitors
{
	[Monitor(IgnoreOthers = false, IgnoreEdits = false)]
	public class SocialCounterMonitor : StructureBase
	{
		public SocialCounterMonitor(Client client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message)
		{
			Console.WriteLine(
				$"Received Message [{message.Id}] from {(await message.GetAuthorAsync(Client))?.Username ?? "??"} with content '{message.Content}'.");
		}
	}
}
