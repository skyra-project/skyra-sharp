using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildEmojiStore : HashMapCacheStoreBase<CoreGuildEmoji>
	{
		internal GuildEmojiStore(CacheClient context) : base(context, "emojis")
		{
		}

		[NotNull]
		protected override string GetKey([NotNull] CoreGuildEmoji value)
		{
			return value.Id.ToString();
		}
	}
}
