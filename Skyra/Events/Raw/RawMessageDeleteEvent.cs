using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events.Raw
{
	[Event]
	public class RawMessageDeleteEvent : StructureBase
	{
		public RawMessageDeleteEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawMessageDeleteAsync += RunAsync;
		}

		private async Task RunAsync([NotNull] MessageDeletePayload messageDeletePayload)
		{
			var message = await Client.Cache.Messages.GetAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId);
			if (message == null)
			{
				await Client.Cache.EditableMessages.DeleteAsync(messageDeletePayload.Id,
					messageDeletePayload.ChannelId);
			}
			else
			{
				await Task.WhenAll(
					Client.Cache.Messages.DeleteAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId),
					Client.Cache.EditableMessages.DeleteAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId));
			}

			await Client.EventHandler.OnMessageDeleteAsync(messageDeletePayload, message);
		}
	}
}
