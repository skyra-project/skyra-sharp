using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events.Raw
{
	[Event]
	public class RawPromptEvent : StructureBase
	{
		public RawPromptEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawPromptAsync += RunAsync;
		}

		private async Task RunAsync(CorePromptState state, object message)
		{
			switch (state.Type)
			{
				case CorePromptStateType.MessageSingleUser:
					await Client.EventHandler.OnRawMessagePromptAsync((CorePromptStateMessage) state.State,
						(CoreMessage) message);
					break;
				case CorePromptStateType.ReactionSingleUser:
					await Client.EventHandler.OnRawReactionPromptAsync((CorePromptStateReaction) state.State,
						(CoreMessageReaction) message);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
