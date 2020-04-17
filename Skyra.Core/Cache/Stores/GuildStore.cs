using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;

namespace Skyra.Core.Cache.Stores
{
	public sealed class GuildStore : HashMapCacheStoreBase<Guild>
	{
		internal GuildStore(CacheClient context) : base(context, "guilds")
		{
		}

		public async Task SetAsync([NotNull] Spectacles.NET.Types.Guild entry, string? parent = null)
		{
			await Task.WhenAll(Context.GuildMembers.SetAsync(entry.Members, entry.Id),
				Context.GuildRoles.SetAsync(entry.Roles.Select(x => GuildRole.From(Context.Client, x)), entry.Id),
				Context.GuildChannels.SetAsync(entry.Channels.Select(x => GuildChannel.From(Context.Client, x)),
					entry.Id),
				Context.VoiceStates.SetAsync(entry.VoiceStates.Select(x => VoiceState.From(Context.Client, x)),
					entry.Id),
				Context.GuildEmojis.SetAsync(entry.Emojis.Select(x => GuildEmoji.From(Context.Client, x)),
					entry.Id),
				SetAsync(Guild.From(Context.Client, entry), parent));
		}

		[NotNull]
		protected override string GetKey([NotNull] Guild value)
		{
			return value.Id.ToString();
		}
	}
}
