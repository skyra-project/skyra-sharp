using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildEmojiStore : HashMapCacheStoreBase<CoreGuildEmoji>
	{
		internal GuildEmojiStore(CacheClient client) : base(client, "emojis")
		{
		}

		protected override string GetKey(CoreGuildEmoji value)
		{
			return value.Id.ToString();
		}
	}
}
