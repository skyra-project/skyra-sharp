using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class MessageDeleteEvent
	{
		private readonly Client _client;

		public MessageDeleteEvent(Client client)
		{
			_client = client;
			_client.EventHandler.OnMessageDelete += Run;
		}

		private void Run(MessageDeletePayload messageDeletePayload)
		{
			Task.Run(() => RunAsync(messageDeletePayload));
		}

		private async Task RunAsync(MessageDeletePayload messageDeletePayload)
		{
			await Task.WhenAll(
				_client.Cache.Messages.DeleteAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId),
				_client.Cache.EditableMessages.DeleteAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId));
		}
	}
}
