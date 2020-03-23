using System;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models.Prompts;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class PromptStore : SetCacheStoreBase<CorePromptState>
	{
		internal PromptStore(CacheClient context) : base(context, "prompts")
		{
		}

		public async Task SetAsync(CorePromptState entry, TimeSpan duration, string? parent = null)
		{
			var id = FormatKeyName(parent, GetKey(entry));
			await Database.StringSetAsync(id, SerializeValue(entry));
			await Database.KeyExpireAsync(id, duration);
		}

		protected override string GetKey(CorePromptState value)
		{
			return value.State.ToKey();
		}
	}
}
