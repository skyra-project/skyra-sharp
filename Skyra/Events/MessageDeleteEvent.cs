using Skyra.Core;
using Skyra.Core.Cache.Models;
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

		private void Run(MessageDeletePayload payload, CoreMessage? message)
		{
			Client.Logger.Information(
				"Received Deleted Message [{Id}] with content '{Content}'.", payload.Id,
				message?.Content ?? "Unknown.");
		}
	}
}
