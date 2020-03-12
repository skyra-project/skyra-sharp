using System.Linq;
using System.Threading.Tasks;
using Skyra.Core.Cache.Models;
using Skyra.Core.Cache.Stores.Base;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Stores
{
	public class GuildStore : HashMapCacheStoreBase<CoreGuild>
	{
		public GuildStore(CacheClient client) : base(client, "guilds")
		{
		}

		public async Task SetAsync(Guild entry, string? parent = null)
		{
			await Task.WhenAll(Client.GuildMembers.SetAsync(entry.Members, entry.Id),
				Client.GuildRoles.SetAsync(entry.Roles.Select(CoreGuildRole.From), entry.Id),
				Client.GuildChannels.SetAsync(entry.Channels.Select(CoreGuildChannel.From), entry.Id),
				Client.VoiceStates.SetAsync(entry.VoiceStates.Select(CoreVoiceState.From), entry.Id),
				Client.GuildEmojis.SetAsync(entry.Emojis.Select(CoreGuildEmoji.From), entry.Id),
				SetAsync(CoreGuild.From(entry), parent));
		}

		public override string GetKey(CoreGuild value)
		{
			return value.Id.ToString();
		}
	}
}
