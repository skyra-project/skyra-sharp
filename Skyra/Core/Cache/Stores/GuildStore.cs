using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildStore : HashMapCacheStoreBase<CoreGuild>
	{
		internal GuildStore(CacheClient context) : base(context, "guilds")
		{
		}

		public async Task SetAsync(Guild entry, string? parent = null)
		{
			await Task.WhenAll(Context.GuildMembers.SetAsync(entry.Members, entry.Id),
				Context.GuildRoles.SetAsync(entry.Roles.Select(x => CoreGuildRole.From(Context.Client, x)), entry.Id),
				Context.GuildChannels.SetAsync(entry.Channels.Select(x => CoreGuildChannel.From(Context.Client, x)),
					entry.Id),
				Context.VoiceStates.SetAsync(entry.VoiceStates.Select(x => CoreVoiceState.From(Context.Client, x)),
					entry.Id),
				Context.GuildEmojis.SetAsync(entry.Emojis.Select(x => CoreGuildEmoji.From(Context.Client, x)),
					entry.Id),
				SetAsync(CoreGuild.From(Context.Client, entry), parent));
		}

		protected override string GetKey(CoreGuild value)
		{
			return value.Id.ToString();
		}
	}
}
