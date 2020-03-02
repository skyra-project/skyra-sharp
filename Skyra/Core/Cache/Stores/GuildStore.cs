using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class GuildStore : CacheStore<CoreGuild>
	{
		public GuildStore(CacheClient client) : base(client, "guilds")
		{
		}

		public Task SetAsync(Guild entry, string? parent = null)
		{
			return Task.WhenAll(Client.GuildMembers.SetAsync(entry.Members, entry.Id),
				Client.GuildRoles.SetAsync(entry.Roles, entry.Id),
				Client.GuildChannels.SetAsync(entry.Channels, entry.Id),
				Client.VoiceStates.SetAsync(entry.VoiceStates, entry.Id),
				Client.GuildEmojis.SetAsync(entry.Emojis, entry.Id),
				SetAsync(new CoreGuild(entry), parent));
		}

		public override Task SetAsync(CoreGuild entry, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public override async Task SetAsync(IEnumerable<CoreGuild> entries, string? parent = null)
		{
			var guilds = entries as CoreGuild[] ?? entries.ToArray();
			if (parent != null)
			{
				var unboxedIds = guilds.Select(entry => RedisValue.Unbox(entry.Id));
				await Database.SetAddAsync(FormatKeyName(parent), unboxedIds.ToArray());
			}

			await Database.HashSetAsync(FormatKeyName(parent),
				guilds.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
