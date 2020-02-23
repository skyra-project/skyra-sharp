using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class GuildCreateEvent : StructureBase
	{
		public GuildCreateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnGuildCreate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => Client.Cache.Guilds.SetAsync(guild));
		}
	}
}
