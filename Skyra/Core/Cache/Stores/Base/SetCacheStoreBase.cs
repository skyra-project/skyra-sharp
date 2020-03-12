using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Core.Cache.Models;

namespace Skyra.Core.Cache.Stores.Base
{
	public abstract class SetCacheStoreBase<T> : CacheStoreBase<T> where T : class, ICoreBaseStructure<T>
	{
		protected SetCacheStoreBase(CacheClient client, string prefix) : base(client, prefix)
		{
		}

		public override async Task<T?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.StringGetAsync(FormatKeyName(parent, id));
			return !result.IsNull ? JsonConvert.DeserializeObject<T>(result.ToString()) : null;
		}

		public override async Task SetAsync(T entry, string? parent = null)
		{
			await Database.StringSetAsync(FormatKeyName(parent, GetKey(entry)), SerializeValue(entry));
		}

		public override async Task SetAsync(IEnumerable<T> entries, string? parent = null)
		{
			await Task.WhenAll(entries.Select(entry => SetAsync(entry, parent)));
		}
	}
}
