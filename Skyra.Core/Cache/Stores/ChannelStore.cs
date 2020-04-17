using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public sealed class ChannelStore : HashMapCacheStoreBase<Channel>
	{
		internal ChannelStore(CacheClient context) : base(context, "channels")
		{
		}

		public override async Task SetAsync(Channel entry, [CanBeNull] string? parent = null)
		{
			if (parent != null) await Database.StringSetAsync(FormatKeyName(parent), RedisValue.Unbox(entry.Id));
			await Database.HashSetAsync(Prefix, new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		[NotNull]
		protected override string GetKey([NotNull] Channel value)
		{
			return value.Id.ToString();
		}
	}
}
