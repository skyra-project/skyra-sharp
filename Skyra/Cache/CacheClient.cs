using System.Linq;
using System.Threading.Tasks;
using Skyra.Cache.Stores;
using Spectacles.NET.Rest.Redis;
using StackExchange.Redis;

namespace Skyra.Cache
{
	public class CacheClient
	{
		public string Prefix { get; }
		public ConnectionPool Pool { get; } = new ConnectionPool();

		public ConnectionMultiplexer BestConnection => Pool.BestConnection;

		public IDatabase Database => BestConnection.GetDatabase();

		public IServer Redis => BestConnection.GetEndPoints().Select(endPoint => BestConnection.GetServer(endPoint))
			.FirstOrDefault(server => !server.IsSlave);

		public ChannelStore Channels { get; }

		public GuildStore Guilds { get; }

		public CacheClient(string prefix)
		{
			Prefix = prefix;
			Channels = new ChannelStore(this);
			Guilds = new GuildStore(this);
		}

		public async Task ConnectAsync(string url)
		{
			var options = ConfigurationOptions.Parse(url);
			await Pool.ConnectAsync(options);
		}
	}
}
