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
			Client.EventHandler.OnRawMessageUpdateAsync += RunAsync;
		}

		private async Task RunAsync(MessageUpdatePayload messageUpdate)
		{
			// Instantiate CoreMessage for usage everywhere
			var previousMessage = await Client.Cache.Messages.GetAsync(messageUpdate.Id, messageUpdate.ChannelId);
			var message = previousMessage == null
				? CoreMessage.From(Client, messageUpdate)
				: previousMessage.Clone().Patch(messageUpdate);
			await message.CacheAsync();

			// Handle the message
			await Client.EventHandler.OnMessageUpdateAsync(previousMessage, message);
		}
	}
}
