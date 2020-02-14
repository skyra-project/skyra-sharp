using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class GuildStore : CacheStore<CachedGuild>
	{
		public GuildStore(CacheClient client) : base(client, "guilds")
		{
		}

		public Task SetAsync(Guild entry, string? parent = null)
			=> Task.WhenAll(Client.Members.SetAsync(entry.Members, entry.Id),
				Client.Roles.SetAsync(entry.Roles, entry.Id),
				Client.Channels.SetAsync(entry.Channels, entry.Id),
				Client.VoiceStates.SetAsync(entry.VoiceStates, entry.Id),
				Client.Emojis.SetAsync(entry.Emojis, entry.Id),
				SetAsync(new CachedGuild(entry), parent));

		public override Task SetAsync(CachedGuild entry, string? parent = null)
			=> Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});

		public override async Task SetAsync(IEnumerable<CachedGuild> entries, string? parent = null)
		{
			var guilds = entries as CachedGuild[] ?? entries.ToArray();
			if (parent != null)
			{
				var unboxedIds = guilds.Select(entry => RedisValue.Unbox(entry.Id));
				await Database.SetAddAsync(FormatKeyName(parent), unboxedIds.ToArray());
			}

			await Database.HashSetAsync(Prefix,
				guilds.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
