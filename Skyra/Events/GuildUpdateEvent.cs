using System.Threading.Tasks;
using Skyra.Core;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class GuildUpdateEvent
	{
		private readonly Client _client;

		public GuildUpdateEvent(Client client)
		{
			_client = client;
			_client.EventHandler.OnGuildUpdate += Run;
		}

		private void Run(Guild guild)
		{
			Task.Run(() => _client.Cache.Guilds.SetAsync(guild));
		}
	}
}
