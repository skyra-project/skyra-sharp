using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events.Raw
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
			Task.Run(() => Client.Cache.Guilds.PatchAsync(CoreGuild.From(guild), guild.Id));
		}
	}
}
