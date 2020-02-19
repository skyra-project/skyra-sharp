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

		public Task SetAsync(GuildMember entry, string? parent = null)
		{
			return Task.WhenAll(Client.Users.SetAsync(entry.User), SetAsync(new CachedGuildMember(entry), parent));
		}

		public override Task SetAsync(CachedGuildMember entry, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public Task SetAsync(IEnumerable<GuildMember> entries, string? parent = null)
		{
			var users = new List<CachedUser>();
			var members = new List<CachedGuildMember>();
			foreach (var entry in entries)
			{
				users.Add(new CachedUser(entry.User));
				members.Add(new CachedGuildMember(entry));
			}

			return Task.WhenAll(Client.Users.SetAsync(users), SetAsync(members, parent));
		}

		public override Task SetAsync(IEnumerable<CachedGuildMember> entries, string? parent = null)
		{
			return Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
