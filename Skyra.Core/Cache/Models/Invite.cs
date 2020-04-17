using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class Invite : IBaseStructure<Invite>
	{
		public Invite(IClient client, string code, ulong guildId, ulong channelId)
		{
			Client = client;
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

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public Invite Patch(Invite value)
		{
			return this;
		}

		[NotNull]
		public Invite Clone()
		{
			return new Invite(Client,
				Code,
				GuildId,
				ChannelId);
		}

		[NotNull]
		public static Invite From(IClient client, [NotNull] Spectacles.NET.Types.Invite invite)
		{
			return new Invite(client, invite.Code, ulong.Parse(invite.Guild.Id), ulong.Parse(invite.Channel.Id));
		}

		[ItemCanBeNull]
		public async Task<Guild?> GetGuildAsync()
		{
			return await Client.Cache.Guilds.GetAsync(GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<GuildChannel?> GetChannelAsync()
		{
			return await Client.Cache.GuildChannels.GetAsync(ChannelId.ToString());
		}
	}
}
