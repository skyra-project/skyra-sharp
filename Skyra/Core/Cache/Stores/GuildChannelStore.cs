using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class GuildChannelStore : CacheStore<CoreGuildChannel>
	{
		public GuildChannelStore(CacheClient client) : base(client, "channels")
		{
		}

		public override async Task SetAsync(CoreGuildChannel entry, string? parent = null)
		{
			if (parent != null) await Database.StringSetAsync(FormatKeyName(parent), RedisValue.Unbox(entry.Id));
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<Channel> entries, string? parent = null)
		{
			var channels = entries as Channel[] ?? entries.ToArray();
			await SetAsync(channels.Select(c => new CoreGuildChannel(c)), parent);
		}

		public override async Task SetAsync(IEnumerable<CoreGuildChannel> entries, string? parent = null)
		{
			var channels = entries as CoreGuildChannel[] ?? entries.ToArray();
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
