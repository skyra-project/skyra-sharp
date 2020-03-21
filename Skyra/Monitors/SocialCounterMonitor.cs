using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Base;

namespace Skyra.Monitors
{
	[Monitor(IgnoreOthers = false, IgnoreEdits = false)]
	public class SocialCounterMonitor : StructureBase, IMonitor
	{
		public SocialCounterMonitor(IClient client) : base(client)
		{
		}

		public async Task RunAsync(CoreMessage message)
		{
			Client.Logger.Information(
				"Received Message [{Id}] from {Username} with content '{Content}'.", message.Id,
				(await message.GetAuthorAsync())?.Username ?? "??", message.Content);
		}
	}
}
