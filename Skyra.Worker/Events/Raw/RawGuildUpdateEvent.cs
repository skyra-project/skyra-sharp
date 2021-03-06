using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Worker.Events.Raw
{
	[Event]
	public class RawGuildUpdateEvent : StructureBase
	{
		public RawGuildUpdateEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawGuildUpdate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => Client.Cache.Guilds.PatchAsync(Core.Cache.Models.Guild.From(Client, guild), guild.Id));
		}
	}
}
