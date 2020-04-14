using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildRoleStore : HashMapCacheStoreBase<CoreGuildRole>
	{
		internal GuildRoleStore(CacheClient context) : base(context, "roles")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] CoreGuildRole value)
		{
			return value.Id.ToString();
		}
	}
}
