using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class VoiceState : IBaseStructure<VoiceState>
	{
		public VoiceState(IClient client, string sessionId, bool deaf, bool mute, bool suppress, ulong userId,
			ulong channelId, ulong guildId, bool selfDeaf, bool selfMute)
		{
			Client = client;
			SessionId = sessionId;
			Deaf = deaf;
			Mute = mute;
			Suppress = suppress;
			UserId = userId;
			ChannelId = channelId;
			GuildId = guildId;
			SelfDeaf = selfDeaf;
			SelfMute = selfMute;
		}

		[JsonProperty("sid")]
		public string SessionId { get; private set; }

		[JsonProperty("d")]
		public bool Deaf { get; private set; }

		[JsonProperty("m")]
		public bool Mute { get; private set; }

		[JsonProperty("s")]
		public bool Suppress { get; private set; }

		[JsonProperty("uid")]
		public ulong UserId { get; private set; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; private set; }

		[JsonProperty("gid")]
		public ulong GuildId { get; private set; }

		[JsonProperty("sd")]
		public bool SelfDeaf { get; private set; }

		[JsonProperty("sm")]
		public bool SelfMute { get; private set; }

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public VoiceState Patch([NotNull] VoiceState value)
		{
			SessionId = value.SessionId;
			Deaf = value.Deaf;
			Mute = value.Mute;
			Suppress = value.Suppress;
			ChannelId = value.ChannelId;
			SelfDeaf = value.SelfDeaf;
			SelfMute = value.SelfMute;
			return this;
		}

		[NotNull]
		public VoiceState Clone()
		{
			return new VoiceState(Client,
				SessionId,
				Deaf,
				Mute,
				Suppress,
				UserId,
				ChannelId,
				GuildId,
				SelfDeaf,
				SelfMute);
		}

		[ItemCanBeNull]
		public async Task<Guild?> GetGuildAsync()
		{
			return await Client.Cache.Guilds.GetAsync(GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<GuildChannel?> GetChannelAsync()
		{
			return await Client.Cache.GuildChannels.GetAsync(GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<User?> GetUserAsync()
		{
			return await Client.Cache.Users.GetAsync(UserId.ToString());
		}

		[NotNull]
		public static VoiceState From(IClient client, [NotNull] Spectacles.NET.Types.VoiceState voiceState)
		{
			return new VoiceState(client, voiceState.SessionId, voiceState.Deaf, voiceState.Mute,
				voiceState.Suppress,
				ulong.Parse(voiceState.UserId), ulong.Parse(voiceState.ChannelId), ulong.Parse(voiceState.UserId),
				voiceState.SelfDeaf, voiceState.SelfMute);
		}
	}
}
