using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events.Raw
{
	[Event]
	public class RawGuildDeleteEvent : StructureBase
	{
		public RawGuildDeleteEvent(Client client) : base(client)
		{
			Client.EventHandler.OnRawGuildDelete += Run;
		}

		private void Run(UnavailableGuild guild)
		{
			Task.Run(() => Client.Cache.Guilds.DeleteAsync(guild.Id));
		}
	}
}
