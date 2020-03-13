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
			Client.Logger.Information(
				"Received Message [{Id}] from {Username} with content '{Content}'.", message.Id,
				(await message.GetAuthorAsync(Client))?.Username ?? "??", message.Content);
		}
	}
}
