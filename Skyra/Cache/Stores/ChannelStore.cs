using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class ChannelStore : CacheStore<Channel>
	{
		public ChannelStore(CacheClient client) : base(client, "channels")
		{
		}

		public override async Task SetAsync(Channel entry, string? parent = null)
		{
			if (parent != null) await Database.StringSetAsync(FormatKeyName(parent), RedisValue.Unbox(entry.Id));
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public override async Task SetAsync(IEnumerable<Channel> entries, string? parent = null)
		{
			var channels = entries as Channel[] ?? entries.ToArray();
			if (parent != null)
			{
				var unboxedIds = channels.Select(entry => RedisValue.Unbox(entry.Id));
				await Database.SetAddAsync(FormatKeyName(parent), unboxedIds.ToArray());
			}

			await Database.HashSetAsync(Prefix,
				channels.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
