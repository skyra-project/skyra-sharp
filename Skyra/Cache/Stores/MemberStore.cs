using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class MemberStore : CacheStore<CachedGuildMember>
	{
		public MemberStore(CacheClient client) : base(client, "members")
		{
		}

		public async Task SetAsync(GuildMember entry, string? parent = null)
		{
			await Task.WhenAll(Client.Users.SetAsync(entry.User), SetAsync(new CachedGuildMember(entry), parent));
		}

		public override async Task SetAsync(CachedGuildMember entry, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<GuildMember> entries, string? parent = null)
		{
			var users = new List<CachedUser>();
			var members = new List<CachedGuildMember>();
			foreach (var entry in entries)
			{
				users.Add(new CachedUser(entry.User));
				members.Add(new CachedGuildMember(entry));
			}

			await Task.WhenAll(Client.Users.SetAsync(users), SetAsync(members, parent));
		}

		public override async Task SetAsync(IEnumerable<CachedGuildMember> entries, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
