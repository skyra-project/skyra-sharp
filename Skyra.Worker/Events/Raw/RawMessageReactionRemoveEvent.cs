using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public sealed class RawMessageReactionRemoveEvent : StructureBase
	{
		public RawMessageReactionRemoveEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageReactionRemoveAsync += RunAsync;
		}

		private Task RunAsync(MessageReactionRemovePayload state)
		{
			return Task.CompletedTask;
		}
	}
}
