using System;
using Skyra.Framework;
using Skyra.Framework.Models.Gateway;
using Skyra.Framework.Structures;

namespace Skyra.Events
{
	public class EventMessage : Event
	{
		public EventMessage(Client client) : base(client, new EventOptions {Name = "EventMessage"})
		{
			client.OnMessageCreate += Run;
		}

		private static void Run(object sender, OnMessageCreateArgs args)
		{
			Console.WriteLine($"Received Message [{args.Data.Id}] from {args.Data.Author.Username}.");
		}
	}
}
