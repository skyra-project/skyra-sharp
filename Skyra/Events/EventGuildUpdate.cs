using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventGuildUpdate : Event
	{
		public EventGuildUpdate(Client client) : base(client, new EventOptions(nameof(EventGuildUpdate)))
		{
			EventHandler.OnGuildUpdate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => Client.Cache.Guilds.SetAsync(guild));
		}
	}
}
