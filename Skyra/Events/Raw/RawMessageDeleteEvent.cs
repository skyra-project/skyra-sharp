using System.Threading.Tasks;
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
			Client.EventHandler.OnRawMessageDelete += Run;
		}

		private void Run(MessageDeletePayload messageDeletePayload)
		{
			Task.Run(() => RunAsync(messageDeletePayload));
		}

		private async Task RunAsync(MessageDeletePayload messageDeletePayload)
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

			Client.EventHandler.OnMessageDelete(messageDeletePayload, message);
		}
	}
}
