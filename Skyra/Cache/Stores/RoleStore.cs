using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class RoleStore : CacheStore<Role>
	{
		public RoleStore(CacheClient client) : base(client, "roles")
		{
		}

		public override Task SetAsync(Role entry, string? parent = null)
			=> Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});

		public override Task SetAsync(IEnumerable<Role> entries, string? parent = null)
			=> Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
	}
}
