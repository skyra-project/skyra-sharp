using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class GuildRoleStore : CacheStore<CoreGuildRole>
	{
		public GuildRoleStore(CacheClient client) : base(client, "roles")
		{
		}

		public async Task SetAsync(Role entry, string? parent = null)
		{
			await SetAsync(new CoreGuildRole(entry), parent);
		}

		public override Task SetAsync(CoreGuildRole entry, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<Role> entries, string? parent = null)
		{
			await SetAsync(entries.Select(e => new CoreGuildRole(e)), parent);
		}

		public override Task SetAsync(IEnumerable<CoreGuildRole> entries, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
