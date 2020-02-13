using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class MemberStore : CacheStore<GuildMember>
	{
		public MemberStore(CacheClient client) : base(client, "members")
		{
		}

		public override async Task SetAsync(GuildMember entry, string? parent = null)
		{
			var id = entry.User.Id;
			await Client.Users.SetAsync(entry.User);
			entry.User = null;

			await Database.HashSetAsync(FormatKeyName(parent), new[] {new HashEntry(id, SerializeValue(entry))});
		}

		public override async Task SetAsync(IEnumerable<GuildMember> entries, string? parent = null)
		{
			var users = new List<User>();
			var hashEntries = new List<HashEntry>();

			foreach (var entry in entries)
			{
				var id = entry.User!.Id;
				users.Add(entry.User);
				entry.User = null;
				hashEntries.Add(new HashEntry(id, SerializeValue(entry)));
			}

			await Task.WhenAll(Client.Users.SetAsync(users),
				Database.HashSetAsync(FormatKeyName(parent), hashEntries.ToArray()));
		}
	}
}
