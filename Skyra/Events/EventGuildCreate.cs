using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventGuildCreate : Event
	{
		public EventGuildCreate(Client client) : base(client, new EventOptions(nameof(EventMessageCreate)))
		{
			EventHandler.OnGuildCreate += Run;
		}

		private void Run(Guild guild)
			=> Task.Run(() => Client.Cache.Guilds.SetAsync(guild));
	}
}
