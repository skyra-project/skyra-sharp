using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreInvite : ICoreBaseStructure<CoreInvite>
	{
		public CoreInvite(IClient client, string code, ulong guildId, ulong channelId)
		{
			Client = client;
			Code = code;
			GuildId = guildId;
			ChannelId = channelId;
		}

		[JsonProperty("c")] public string Code { get; set; }

		[JsonProperty("gid")] public ulong GuildId { get; set; }

		[JsonProperty("cid")] public ulong ChannelId { get; set; }

		[JsonIgnore] public IClient Client { get; set; }

		public CoreInvite Patch(CoreInvite value)
		{
			return this;
		}

		public CoreInvite Clone()
		{
			return new CoreInvite(Client,
				Code,
				GuildId,
				ChannelId);
		}

		public static CoreInvite From(IClient client, Invite invite)
		{
			return new CoreInvite(client, invite.Code, ulong.Parse(invite.Guild.Id), ulong.Parse(invite.Channel.Id));
		}

		public async Task<CoreGuild?> GetGuildAsync()
		{
			return await Client.Cache.Guilds.GetAsync(GuildId.ToString());
		}

		public async Task<CoreGuildChannel?> GetChannelAsync()
		{
			return await Client.Cache.GuildChannels.GetAsync(ChannelId.ToString());
		}
	}
}
