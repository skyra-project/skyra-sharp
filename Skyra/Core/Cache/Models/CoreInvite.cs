using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreInvite : ICoreBaseStructure<CoreInvite>
	{
		public CoreInvite(string code, ulong guildId, ulong channelId)
		{
			Code = code;
			GuildId = guildId;
			ChannelId = channelId;
		}

		[JsonProperty("c")]
		public string Code { get; set; }

		[JsonProperty("gid")]
		public ulong GuildId { get; set; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; set; }

		public CoreInvite Patch(CoreInvite value)
		{
			return this;
		}

		public CoreInvite Clone()
		{
			return new CoreInvite(Code,
				GuildId,
				ChannelId);
		}

		public static CoreInvite From(Invite invite)
		{
			return new CoreInvite(invite.Code, ulong.Parse(invite.Guild.Id), ulong.Parse(invite.Channel.Id));
		}

		public async Task<CoreGuild?> GetGuildAsync(Client client)
		{
			return await client.Cache.Guilds.GetAsync(GuildId.ToString());
		}

		public async Task<CoreGuildChannel?> GetChannelAsync(Client client)
		{
			return await client.Cache.GuildChannels.GetAsync(ChannelId.ToString());
		}
	}
}
