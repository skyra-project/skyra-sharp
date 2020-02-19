﻿using System.Linq;
using System.Threading.Tasks;
using Skyra.Cache.Stores;
using Spectacles.NET.Rest.Redis;
using StackExchange.Redis;

namespace Skyra.Cache
{
	public class CacheClient
	{
		public CacheClient(string prefix)
		{
			Prefix = prefix;
			Channels = new ChannelStore(this);
			Emojis = new EmojiStore(this);
			Guilds = new GuildStore(this);
			Members = new MemberStore(this);
			Messages = new MessageStore(this);
			Roles = new RoleStore(this);
			Users = new UserStore(this);
			VoiceStates = new VoiceStateStore(this);
		}

		public string Prefix { get; }
		public ConnectionPool Pool { get; } = new ConnectionPool();

		public ConnectionMultiplexer BestConnection => Pool.BestConnection;

		public IDatabase Database => BestConnection.GetDatabase();

		public IServer Redis => BestConnection.GetEndPoints().Select(endPoint => BestConnection.GetServer(endPoint))
			.FirstOrDefault(server => !server.IsSlave);

		public ChannelStore Channels { get; }

		public EmojiStore Emojis { get; }

		public GuildStore Guilds { get; }

		public MemberStore Members { get; }

		public MessageStore Messages { get; }

		public RoleStore Roles { get; }

		public UserStore Users { get; }

		public VoiceStateStore VoiceStates { get; }

		public async Task ConnectAsync(string url)
		{
			var options = ConfigurationOptions.Parse(url);
			await Pool.ConnectAsync(options);
		}
	}
}
