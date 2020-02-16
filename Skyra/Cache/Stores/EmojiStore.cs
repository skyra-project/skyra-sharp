using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class EmojiStore : CacheStore<Emoji>
	{
		public EmojiStore(CacheClient client) : base(client, "emojis")
		{
		}

		public override Task SetAsync(Emoji entry, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public override Task SetAsync(IEnumerable<Emoji> entries, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
