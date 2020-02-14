using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventGuildUpdate : Event
	{
		public EventGuildUpdate(Client client) : base(client, new EventOptions(nameof(EventMessageCreate)))
		{
			EventHandler.OnGuildUpdate += Run;
		}

		private void Run(Guild guild)
			=> Task.Run(() => Client.Cache.Guilds.SetAsync(guild));
	}
}
