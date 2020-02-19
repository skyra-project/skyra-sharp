using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public abstract class CacheStore<T> where T : class
	{
		protected CacheStore(CacheClient client, string prefix)
		{
			Client = client;
			Prefix = $"{Client.Prefix}:{prefix}";
		}

		protected CacheClient Client { get; }
		protected IDatabase Database => Client.Database;
		protected string Prefix { get; }

		public async Task<T?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.HashGetAsync(FormatKeyName(parent), id);
			return !result.IsNull ? JsonConvert.DeserializeObject<T>(result.ToString()) : null;
		}

		public Task<T?[]> GetAsync(IEnumerable<string> ids, string? parent = null)
		{
			return Task.WhenAll(ids.Select(id => GetAsync(id, parent)));
		}

		public async Task<T[]> GetAllAsync(string? parent = null)
		{
			var results = await Database.HashGetAllAsync(FormatKeyName(parent));
			return results.Select(result => JsonConvert.DeserializeObject<T>(result.Value.ToString())).ToArray();
		}

		public abstract Task SetAsync(T entry, string? parent = null);

		public abstract Task SetAsync(IEnumerable<T> entries, string? parent = null);

		public async Task DeleteAsync(string id, string? parent = null)
		{
			if (parent != null) await Database.SetRemoveAsync(FormatKeyName(parent), id);
			await Database.HashDeleteAsync(Prefix, id);
		}

		public Task DeleteAsync(IEnumerable<string> ids, string? parent = null)
		{
			return Task.WhenAll(ids.Select(id => DeleteAsync(id, parent)));
		}

		protected string FormatKeyName(string? parent)
		{
			return parent == null ? Prefix : $"{Prefix}:{parent}";
		}

		protected string SerializeValue(T value)
		{
			return JsonConvert.SerializeObject(value, Formatting.None,
				new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
		}
	}
}
