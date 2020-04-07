using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildChannelStore : HashMapCacheStoreBase<CoreGuildChannel>
	{
		internal GuildChannelStore(CacheClient context) : base(context, "gchannels")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] CoreGuildChannel value)
		{
			return value.Id.ToString();
		}
	}
}
