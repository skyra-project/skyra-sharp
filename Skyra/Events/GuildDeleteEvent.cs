using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class GuildDeleteEvent : StructureBase
	{
		public GuildDeleteEvent(Client client) : base(client)
		{
			Client.EventHandler.OnGuildDelete += Run;
		}

		private void Run(UnavailableGuild guild)
		{
			Task.Run(() => Client.Cache.Guilds.DeleteAsync(guild.Id));
		}
	}
}
