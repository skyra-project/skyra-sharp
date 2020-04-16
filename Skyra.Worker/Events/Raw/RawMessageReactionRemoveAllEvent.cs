using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public sealed class RawMessageReactionRemoveAllEvent : StructureBase
	{
		public RawMessageReactionRemoveAllEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageReactionRemoveAllAsync += RunAsync;
		}

		private Task RunAsync(MessageReactionRemoveAllPayload state)
		{
			return Task.CompletedTask;
		}
	}
}
