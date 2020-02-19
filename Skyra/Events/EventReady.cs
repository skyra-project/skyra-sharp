using System;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventReady : Event
	{
		public EventReady(Client client) : base(client, new EventOptions(nameof(EventReady)))
		{
			EventHandler.OnReady += Run;
		}

		private static void Run(ReadyDispatch args)
		{
			Console.WriteLine(
				$"Skyra VI ready! [{args.User.Username}#{args.User.Discriminator}] [{args.Guilds.Length.ToString()} [G]]");
		}
	}
}
