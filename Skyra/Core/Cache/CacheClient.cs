using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Stores;
using Spectacles.NET.Rest.Redis;
using StackExchange.Redis;

namespace Skyra.Core.Cache
{
	public class CacheClient
	{
		public CacheClient(string prefix)
		{
			Prefix = prefix;
			GuildChannels = new GuildChannelStore(this);
			EditableMessages = new EditableMessagesStore(this);
			GuildEmojis = new GuildEmojiStore(this);
			Guilds = new GuildStore(this);
			GuildMembers = new GuildMemberStore(this);
			Messages = new MessageStore(this);
			GuildRoles = new GuildRoleStore(this);
			Users = new UserStore(this);
			VoiceStates = new VoiceStateStore(this);
		}

		public string Prefix { get; }
		public ConnectionPool Pool { get; } = new ConnectionPool();

		public ConnectionMultiplexer BestConnection => Pool.BestConnection;

		public IDatabase Database => BestConnection.GetDatabase();

		public IServer Redis => BestConnection.GetEndPoints().Select(endPoint => BestConnection.GetServer(endPoint))
			.FirstOrDefault(server => !server.IsSlave);

		public GuildChannelStore GuildChannels { get; }

		public EditableMessagesStore EditableMessages { get; }

		public GuildEmojiStore GuildEmojis { get; }

		public GuildStore Guilds { get; }

		public GuildMemberStore GuildMembers { get; }

		public MessageStore Messages { get; }

		public GuildRoleStore GuildRoles { get; }

		public UserStore Users { get; }

		public VoiceStateStore VoiceStates { get; }

		public async Task ConnectAsync(string url)
		{
			var options = ConfigurationOptions.Parse(url);
			await Pool.ConnectAsync(options);
		}

		public async Task SetClientUser(string id)
		{
			await Database.StringSetAsync($"{Prefix}:CLIENT_ID", id);
		}

		public async Task<string> GetClientUser()
		{
			return await Database.StringGetAsync($"{Prefix}:CLIENT_ID");
		}
	}
}
