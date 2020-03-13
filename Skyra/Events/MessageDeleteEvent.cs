using System;
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

		private static void Run(MessageDeletePayload payload, CoreMessage? message)
		{
			Console.WriteLine(
				$"Received Deleted Message [{payload.Id}] with content '{message?.Content ?? "Unknown."}'.");
		}
	}
}
