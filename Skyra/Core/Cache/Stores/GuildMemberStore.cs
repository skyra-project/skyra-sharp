using System.Collections.Generic;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Stores
{
	public class GuildMemberStore : HashMapCacheStoreBase<CoreGuildMember>
	{
		public GuildMemberStore(CacheClient client) : base(client, "members")
		{
		}

		public async Task SetAsync(IEnumerable<GuildMember> entries, string? parent = null)
		{
			var users = new List<CoreUser>();
			var members = new List<CoreGuildMember>();
			foreach (var entry in entries)
			{
				users.Add(CoreUser.From(entry.User));
				members.Add(CoreGuildMember.From(entry));
			}

			await Task.WhenAll(Client.Users.SetAsync(users), SetAsync(members, parent));
		}

		protected override string GetKey(CoreGuildMember value)
		{
			return value.Id.ToString();
		}
	}
}
