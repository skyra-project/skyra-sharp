using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events
{
	[Event]
	public class MessageDeleteEvent : StructureBase
	{
		public MessageDeleteEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnMessageDeleteAsync += RunAsync;
		}

		private Task RunAsync([NotNull] MessageDeletePayload payload, [CanBeNull] CoreMessage? message)
		{
			Client.Logger.Information(
				"Received Deleted Message [{Id}] with content '{Content}'.", payload.Id,
				message?.Content ?? "Unknown.");

			return Task.CompletedTask;
		}
	}
}
