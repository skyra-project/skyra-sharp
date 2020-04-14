using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores.Base
{
	public abstract class HashMapCacheStoreBase<T> : CacheStoreBase<T> where T : class, ICoreBaseStructure<T>
	{
		protected HashMapCacheStoreBase(CacheClient context, string prefix) : base(context, prefix)
		{
		}

		[ItemCanBeNull]
		public override async Task<T?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.HashGetAsync(FormatKeyName(parent), id);
			return !result.IsNull ? DeserializeValue(result.ToString()) : null;
		}

		[ItemNotNull]
		public override async Task<T[]> GetAllAsync(string? parent = null)
		{
			var results = await Database.HashGetAllAsync(FormatKeyName(parent));
			return results.Select(result => DeserializeValue(result.Value.ToString())).ToArray();
		}

		public override async Task SetAsync(T entry, string? parent = null)
		{
			var id = GetKey(entry);
			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(id, SerializeValue(entry))});
		}

		public override async Task SetAsync([NotNull] IEnumerable<T> entries, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(GetKey(entry), SerializeValue(entry))).ToArray());
		}

		public override async Task DeleteAsync(string id, [CanBeNull] string? parent = null)
		{
			if (parent != null) await Database.SetRemoveAsync(FormatKeyName(parent), id);
			await Database.HashDeleteAsync(Prefix, id);
		}
	}
}
