using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;

namespace Skyra.Core.Cache.Stores.Base
{
	public abstract class SetCacheStoreBase<T> : CacheStoreBase<T> where T : class, IBaseStructure<T>
	{
		protected SetCacheStoreBase(CacheClient context, string prefix) : base(context, prefix)
		{
		}

		[ItemCanBeNull]
		public override async Task<T?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.StringGetAsync(FormatKeyName(parent, id));
			return result.IsNull ? null : DeserializeValue(result.ToString());
		}

		public override Task<T[]> GetAllAsync(string? parent = null)
		{
			throw new NotImplementedException();
		}

		public override async Task SetAsync(T entry, string? parent = null)
		{
			await Database.StringSetAsync(FormatKeyName(parent, GetKey(entry)), SerializeValue(entry));
		}

		public override async Task SetAsync([NotNull] IEnumerable<T> entries, string? parent = null)
		{
			var transaction = Database.CreateTransaction();
			await Task.WhenAll(entries.Select(entry =>
				transaction.StringSetAsync(FormatKeyName(parent, GetKey(entry)), SerializeValue(entry))));
			await transaction.ExecuteAsync();
		}

		public override async Task DeleteAsync(string id, string? parent = null)
		{
			await Database.KeyDeleteAsync(FormatKeyName(parent, id));
		}

		[NotNull]
		protected string FormatKeyName(string? parent, [CanBeNull] string? id)
		{
			return id == null ? FormatKeyName(parent) : $"{FormatKeyName(parent)}:{id}";
		}
	}
}
