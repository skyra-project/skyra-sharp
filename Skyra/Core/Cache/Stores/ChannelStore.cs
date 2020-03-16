using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public sealed class ChannelStore : HashMapCacheStoreBase<CoreChannel>
	{
		internal ChannelStore(CacheClient client) : base(client, "channels")
		{
		}

		public override async Task SetAsync(CoreChannel entry, string? parent = null)
		{
			if (parent != null) await Database.StringSetAsync(FormatKeyName(parent), RedisValue.Unbox(entry.Id));
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		protected override string GetKey(CoreChannel value)
		{
			return value.Id.ToString();
		}
	}
}
