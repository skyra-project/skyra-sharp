using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class GuildCreateEvent
	{
		private readonly Client _client;

		public GuildCreateEvent(Client client)
		{
			_client = client;
			_client.EventHandler.OnGuildCreate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => _client.Cache.Guilds.SetAsync(guild));
		}
	}
}
