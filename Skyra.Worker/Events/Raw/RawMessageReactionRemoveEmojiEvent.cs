using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public sealed class RawMessageReactionRemoveEmojiEvent : StructureBase
	{
		public RawMessageReactionRemoveEmojiEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageReactionRemoveEmojiAsync += RunAsync;
		}

		private Task RunAsync(MessageReactionRemoveEmojiPayload state)
		{
			return Task.CompletedTask;
		}
	}
}
