using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class ReadyEvent : StructureBase
	{
		public ReadyEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnReady += Run;
		}

		private void Run(ReadyDispatch args)
		{
			Task.Run(() => Client.Cache.SetClientUserAsync(args.User.Id));
			Client.Id = ulong.Parse(args.User.Id);
			Client.Logger.Information(
				"Skyra VI ready! [{Username}#{Discriminator}] [{Guilds} [G]]", args.User.Username,
				args.User.Discriminator, args.Guilds.Length);
		}
	}
}
