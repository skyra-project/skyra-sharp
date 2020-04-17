using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildChannelStore : HashMapCacheStoreBase<GuildChannel>
	{
		internal GuildChannelStore(CacheClient context) : base(context, "g_channels")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] GuildChannel value)
		{
			return value.Id.ToString();
		}
	}
}
