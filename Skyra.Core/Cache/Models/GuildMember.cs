using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class GuildMember : IBaseStructure<GuildMember>
	{
		public GuildMember(IClient client, ulong id, ulong[] roles, string? nickname, DateTime? joinedAt, bool deaf,
			bool mute)
		{
			Client = client;
			Id = id;
			Roles = roles;
			Nickname = nickname;
			JoinedAt = joinedAt;
			Deaf = deaf;
			Mute = mute;
		}

		[JsonProperty("id")]
		public ulong Id { get; set; }

		[JsonProperty("r")]
		public ulong[] Roles { get; set; }

		[JsonProperty("n")]
		public string? Nickname { get; set; }

		[JsonProperty("j")]
		public DateTime? JoinedAt { get; set; }

		[JsonProperty("d")]
		public bool Deaf { get; set; }

		[JsonProperty("m")]
		public bool Mute { get; set; }

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public GuildMember Patch([NotNull] GuildMember value)
		{
			Roles = value.Roles;
			Nickname = value.Nickname;
			JoinedAt = value.JoinedAt ?? JoinedAt;
			Deaf = value.Deaf;
			Mute = value.Mute;
			return this;
		}

		[NotNull]
		public GuildMember Clone()
		{
			return new GuildMember(Client,
				Id,
				Roles,
				Nickname,
				JoinedAt,
				Deaf,
				Mute);
		}

		[ItemCanBeNull]
		public async Task<User?> GetUserAsync()
		{
			return await Client.Cache.Users.GetAsync(Id.ToString());
		}

		[NotNull]
		public static GuildMember From(IClient client, [NotNull] Spectacles.NET.Types.GuildMember guildMember,
			Spectacles.NET.Types.User? user = null)
		{
			DateTime? joinedAt;
			if (DateTime.TryParse(guildMember.JoinedAt, out var result))
			{
				joinedAt = result;
			}
			else
			{
				joinedAt = null;
			}

			return new GuildMember(client, ulong.Parse((guildMember.User ?? user!).Id),
				guildMember.Roles.Select(ulong.Parse).ToArray(),
				guildMember.Nickname, joinedAt, guildMember.Deaf, guildMember.Mute);
		}
	}
}
