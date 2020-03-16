using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events.Raw
{
	[Event]
	public class RawGuildCreateEvent : StructureBase
	{
		public RawGuildCreateEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnRawGuildCreate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => Client.Cache.Guilds.SetAsync(guild));
		}
	}
}
