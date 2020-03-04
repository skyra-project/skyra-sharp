using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class ChannelStore : CacheStore<CoreChannel>
	{
		public ChannelStore(CacheClient client) : base(client, "channels")
		{
		}

		public override async Task SetAsync(CoreChannel entry, string? parent = null)
		{
			if (parent != null) await Database.StringSetAsync(FormatKeyName(parent), RedisValue.Unbox(entry.Id));
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<Channel> entries, string? parent = null)
		{
			await SetAsync(entries.Select(c => new CoreChannel(c)), parent);
		}

		public override async Task SetAsync(IEnumerable<CoreChannel> entries, string? parent = null)
		{
			var channels = entries as CoreChannel[] ?? entries.ToArray();
			await Database.HashSetAsync(Prefix,
				channels.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
