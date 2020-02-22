using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Cache.Models;
using Spectacles.NET.Types;

namespace Skyra.Cache.Stores
{
	public class UserStore : CacheStore<CachedUser>
	{
		public UserStore(CacheClient client) : base(client, "users")
		{
		}

		public new async Task<CachedUser?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.StringGetAsync($"{FormatKeyName(parent)}:{id}");
			return !result.IsNull ? JsonConvert.DeserializeObject<CachedUser>(result) : null;
		}

		public Task SetAsync(User entry, string? parent = null)
		{
			return SetAsync(new CachedUser(entry), parent);
		}

		public override Task SetAsync(CachedUser entry, string? parent = null)
		{
			return Database.StringSetAsync($"{FormatKeyName(parent)}:{entry.Id}", SerializeValue(entry));
		}

		public Task SetAsync(IEnumerable<User> entries, string? parent = null)
		{
			return Task.WhenAll(entries.Select(entry => SetAsync(entry, parent)));
		}

		public override Task SetAsync(IEnumerable<CachedUser> entries, string? parent = null)
		{
			return Task.WhenAll(entries.Select(entry => SetAsync(entry, parent)));
		}
	}
}
