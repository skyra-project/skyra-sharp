using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Core.Cache.Stores
{
	public class GuildMemberStore : CacheStore<CoreGuildMember>
	{
		public GuildMemberStore(CacheClient client) : base(client, "members")
		{
		}

		public async Task SetAsync(GuildMember entry, string? parent = null)
		{
			await Task.WhenAll(Client.Users.SetAsync(entry.User), SetAsync(new CoreGuildMember(entry), parent));
		}

		public override async Task SetAsync(CoreGuildMember entry, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(entry.Id, SerializeValue(entry))});
		}

		public async Task SetAsync(IEnumerable<GuildMember> entries, string? parent = null)
		{
			var users = new List<CoreUser>();
			var members = new List<CoreGuildMember>();
			foreach (var entry in entries)
			{
				users.Add(new CoreUser(entry.User));
				members.Add(new CoreGuildMember(entry));
			}

			await Task.WhenAll(Client.Users.SetAsync(users), SetAsync(members, parent));
		}

		public override async Task SetAsync(IEnumerable<CoreGuildMember> entries, string? parent = null)
		{
			await Database.HashSetAsync(FormatKeyName(parent),
				entries.Select(entry => new HashEntry(entry.Id, SerializeValue(entry))).ToArray());
		}
	}
}
