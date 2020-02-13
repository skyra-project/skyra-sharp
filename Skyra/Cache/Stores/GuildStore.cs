using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class GuildStore : CacheStore<Guild>
	{
		public GuildStore(CacheClient client) : base(client, "guilds")
		{
		}

		public override async Task SetAsync(Guild entry, string? _ = null)
		{
			// Store the guild sub-data into different tables
			await Task.WhenAll(
				Client.Members.SetAsync(entry.Members, entry.Id),
				Client.Roles.SetAsync(entry.Roles, entry.Id),
				Client.Channels.SetAsync(entry.Channels, entry.Id),
				Client.VoiceStates.SetAsync(entry.VoiceStates, entry.Id),
				Client.Emojis.SetAsync(entry.Emojis, entry.Id));

			// Set data that is stored in other Redis keys as null in the entry, so SerializeValue doesn't include them in the data
			entry.Members = null;
			entry.Roles = null;
			entry.Channels = null;
			entry.VoiceStates = null;
			entry.Presences = null;
			entry.Emojis = null;

			// Store the guild data
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public override async Task SetAsync(IEnumerable<Guild> entries, string? parent = null)
		{
			var guilds = entries as Guild[] ?? entries.ToArray();
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
