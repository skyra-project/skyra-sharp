using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class GuildEmojiStore : CacheStore<CoreGuildEmoji>
	{
		public GuildEmojiStore(CacheClient client) : base(client, "emojis")
		{
		}

		public async Task SetAsync(Emoji entry, string? parent = null)
		{
			await SetAsync(new CoreGuildEmoji(entry), parent);
		}

		public override async Task SetAsync(CoreGuildEmoji entry, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<Emoji> entries, string? parent = null)
		{
			await SetAsync(entries.Select(e => new CoreGuildEmoji(e)), parent);
		}

		public override Task SetAsync(IEnumerable<CoreGuildEmoji> entries, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
