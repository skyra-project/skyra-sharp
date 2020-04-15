using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public class RawMessagePromptEvent : StructureBase
	{
		public RawMessagePromptEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessagePromptAsync += RunAsync;
		}

		private Task RunAsync(CorePromptStateMessage state, CoreMessage message)
		{
			return Task.CompletedTask;
		}
	}
}
