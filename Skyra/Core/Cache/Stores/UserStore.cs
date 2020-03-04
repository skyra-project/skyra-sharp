using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Stores
{
	public class UserStore : CacheStore<CoreUser>
	{
		public UserStore(CacheClient client) : base(client, "users")
		{
		}

		public new async Task<CoreUser?> GetAsync(string id, string? parent = null)
		{
			var result = await Database.StringGetAsync($"{FormatKeyName(parent)}:{id}");
			return !result.IsNull ? JsonConvert.DeserializeObject<CoreUser>(result) : null;
		}

		public Task SetAsync(User entry, string? parent = null)
		{
			return SetAsync(new CoreUser(entry), parent);
		}

		public override Task SetAsync(CoreUser entry, string? parent = null)
		{
			return Database.StringSetAsync(FormatKeyName(parent, entry.Id), SerializeValue(entry));
		}

		public override Task SetAsync(IEnumerable<CoreUser> entries, string? parent = null)
		{
			return Task.WhenAll(entries.Select(entry => SetAsync(entry, parent)));
		}
	}
}
