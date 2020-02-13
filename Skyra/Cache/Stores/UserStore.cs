using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class UserStore : CacheStore<User>
	{
		public UserStore(CacheClient client) : base(client, "users")
		{
		}

		public override async Task SetAsync(User entry, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public override async Task SetAsync(IEnumerable<User> entries, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
