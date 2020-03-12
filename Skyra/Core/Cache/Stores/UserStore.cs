using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public class UserStore : SetCacheStoreBase<CoreUser>
	{
		public UserStore(CacheClient client) : base(client, "users")
		{
		}

		public override string GetKey(CoreUser value)
		{
			return value.Id.ToString();
		}
	}
}
