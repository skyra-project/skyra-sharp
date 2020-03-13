using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public class GuildRoleStore : HashMapCacheStoreBase<CoreGuildRole>
	{
		public GuildRoleStore(CacheClient client) : base(client, "roles")
		{
		}

		protected override string GetKey(CoreGuildRole value)
		{
			return value.Id.ToString();
		}
	}
}
