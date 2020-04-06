using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Core.Cache.Models;
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

		public override async Task<CorePromptState?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.StringGetAsync(FormatKeyName(parent, id));
			return result.IsNull ? null : DeserializeValue(result.ToString());
		}

		protected override string GetKey(CorePromptState value)
		{
			return value.State.ToKey();
		}

		private new CorePromptState DeserializeValue(string value)
		{
			var deserialized = JsonConvert.DeserializeObject<CorePromptState>(value, new CorePromptStateConverter())!;
			deserialized.Client = Context.Client;
			return deserialized;
		}
	}
}
