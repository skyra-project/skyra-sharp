using System.Collections.Generic;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildMemberStore : HashMapCacheStoreBase<CoreGuildMember>
	{
		internal GuildMemberStore(CacheClient context) : base(context, "members")
		{
		}

		public async Task SetAsync(IEnumerable<GuildMember> entries, string? parent = null)
		{
			var users = new List<CoreUser>();
			var members = new List<CoreGuildMember>();
			foreach (var entry in entries)
			{
				users.Add(CoreUser.From(Context.Client, entry.User));
				members.Add(CoreGuildMember.From(Context.Client, entry));
			}

			await Task.WhenAll(Context.Users.SetAsync(users), SetAsync(members, parent));
		}

		protected override string GetKey(CoreGuildMember value)
		{
			return value.Id.ToString();
		}
	}
}
