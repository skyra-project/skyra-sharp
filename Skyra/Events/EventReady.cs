using System;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class EventReady
	{
		public EventReady(Client client)
		{
			client.EventHandler.OnReady += Run;
		}

		private static void Run(ReadyDispatch args)
		{
			// TODO(kyranet): Store Skyra's ID from here
			Console.WriteLine(
				$"Skyra VI ready! [{args.User.Username}#{args.User.Discriminator}] [{args.Guilds.Length.ToString()} [G]]");
		}
	}
}
