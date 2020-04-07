using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public sealed class ChannelStore : HashMapCacheStoreBase<CoreChannel>
	{
		internal ChannelStore(CacheClient context) : base(context, "channels")
		{
		}

		public override async Task SetAsync([NotNull] CoreChannel entry, [CanBeNull] string? parent = null)
		{
			if (parent != null) await Database.StringSetAsync(FormatKeyName(parent), RedisValue.Unbox(entry.Id));
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		[NotNull]
		protected override string GetKey([NotNull] CoreChannel value)
		{
			return value.Id.ToString();
		}
	}
}
