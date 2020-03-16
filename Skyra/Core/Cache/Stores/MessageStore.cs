using System;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class MessageStore : SetCacheStoreBase<CoreMessage>
	{
		internal MessageStore(CacheClient client) : base(client, "messages")
		{
		}

		public override async Task SetAsync(CoreMessage entry, string? parent = null)
		{
			var id = FormatKeyName(parent, entry.Id.ToString());
			await Database.StringSetAsync(id, SerializeValue(entry));
			await Database.KeyExpireAsync(id, TimeSpan.FromMinutes(20));
		}

		protected override string GetKey(CoreMessage value)
		{
			return value.Id.ToString();
		}
	}
}
