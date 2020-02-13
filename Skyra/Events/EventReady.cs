using System;
using Skyra.Models.Gateway;
using Skyra.Structures;

namespace Skyra.Events
{
	public class EventReady : Event
	{
		public EventReady(Client client) : base(client, new EventOptions(nameof(EventReady)))
		{
			EventHandler.OnReady += Run;
		}

		private static void Run(OnReadyArgs args)
		{
			Console.WriteLine(
				$"Skyra VI ready! [{args.Data.User.Username}#{args.Data.User.Discriminator}] [{args.Data.Guilds.Length} [G]]");
		}
	}
}
