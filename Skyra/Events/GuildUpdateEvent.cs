using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class GuildUpdateEvent : StructureBase
	{
		public GuildUpdateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnGuildUpdate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => Client.Cache.Guilds.PatchAsync(new CoreGuild(guild), guild.Id));
		}
	}
}
