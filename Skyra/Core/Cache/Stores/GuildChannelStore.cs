using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public class GuildChannelStore : HashMapCacheStoreBase<CoreGuildChannel>
	{
		public GuildChannelStore(CacheClient client) : base(client, "gchannels")
		{
		}

		protected override string GetKey(CoreGuildChannel value)
		{
			return value.Id.ToString();
		}
	}
}
