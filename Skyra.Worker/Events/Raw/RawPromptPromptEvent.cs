using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public class RawPromptPromptEvent : StructureBase
	{
		public RawPromptPromptEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawReactionPromptAsync += RunAsync;
		}

		private Task RunAsync(CorePromptStateReaction state, CoreMessageReaction reaction)
		{
			return Task.CompletedTask;
		}
	}
}
