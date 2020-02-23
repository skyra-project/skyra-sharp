using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class MessageDeleteEvent : StructureBase
	{
		public MessageDeleteEvent(Client client) : base(client)
		{
			Client.EventHandler.OnMessageDelete += Run;
		}

		private void Run(MessageDeletePayload messageDeletePayload)
		{
			Task.Run(() => RunAsync(messageDeletePayload));
		}

		private async Task RunAsync(MessageDeletePayload messageDeletePayload)
		{
			await Task.WhenAll(
				Client.Cache.Messages.DeleteAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId),
				Client.Cache.EditableMessages.DeleteAsync(messageDeletePayload.Id, messageDeletePayload.ChannelId));
		}
	}
}
