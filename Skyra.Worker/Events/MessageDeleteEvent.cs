using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;
using Message = Skyra.Core.Cache.Models.Message;

namespace Skyra.Worker.Events
{
	[Event]
	public class MessageDeleteEvent : StructureBase
	{
		public MessageDeleteEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnMessageDeleteAsync += RunAsync;
		}

		private Task RunAsync([NotNull] MessageDeletePayload payload, [CanBeNull] Message? message)
		{
			Client.Logger.Information(
				"Received Deleted Message [{Id}] with content '{Content}'.", payload.Id,
				message?.Content ?? "Unknown.");

			return Task.CompletedTask;
		}
	}
}
