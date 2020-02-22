using System.Threading.Tasks;
using Skyra.Core;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventGuildUpdate
	{
		private readonly Client _client;

		public EventGuildUpdate(Client client)
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
