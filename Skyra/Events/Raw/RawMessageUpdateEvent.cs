using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events.Raw
{
	[Event]
	public class RawMessageUpdateEvent : StructureBase
	{
		public RawMessageUpdateEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageUpdate += Run;
		}

		private void Run(MessageUpdatePayload message)
		{
			Task.Run(() => RunMonitorsAsync(message));
		}

		private async Task RunMonitorsAsync(MessageUpdatePayload messageUpdate)
		{
			var previousMessage = await Client.Cache.Messages.GetAsync(messageUpdate.Id);
			var message = previousMessage == null
				? CoreMessage.From(messageUpdate)
				: previousMessage.Clone().Patch(messageUpdate);

			await message.CacheAsync(Client);
			Client.EventHandler.OnMessageUpdate(previousMessage, message);
		}
	}
}
