using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildMemberStore : HashMapCacheStoreBase<GuildMember>
	{
		internal GuildMemberStore(CacheClient context) : base(context, "members")
		{
		}

		public async Task SetAsync([NotNull] IEnumerable<Spectacles.NET.Types.GuildMember> entries,
			string? parent = null)
		{
			var users = new List<User>();
			var members = new List<GuildMember>();
			foreach (var entry in entries)
			{
				users.Add(User.From(Context.Client, entry.User));
				members.Add(GuildMember.From(Context.Client, entry));
			}

			await Task.WhenAll(Context.Users.SetAsync(users), SetAsync(members, parent));
		}

		[NotNull]
		protected override string GetKey([NotNull] GuildMember value)
		{
			return value.Id.ToString();
		}
	}
}
