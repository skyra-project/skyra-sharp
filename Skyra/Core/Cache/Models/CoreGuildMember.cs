using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildMember : ICoreBaseStructure<CoreGuildMember>
	{
		public CoreGuildMember(ulong id, List<string> roles, string? nickname, DateTime? joinedAt, bool deaf,
			bool mute)
		{
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
		public List<string> Roles { get; set; }

		[JsonProperty("n")]
		public string? Nickname { get; set; }

		[JsonProperty("j")]
		public DateTime? JoinedAt { get; set; }

		[JsonProperty("d")]
		public bool Deaf { get; set; }

		[JsonProperty("m")]
		public bool Mute { get; set; }

		public CoreGuildMember Patch(CoreGuildMember value)
		{
			Roles = value.Roles;
			Nickname = value.Nickname;
			JoinedAt = value.JoinedAt ?? JoinedAt;
			Deaf = value.Deaf;
			Mute = value.Mute;
			return this;
		}

		public CoreGuildMember Clone()
		{
			return new CoreGuildMember(Id,
				Roles,
				Nickname,
				JoinedAt,
				Deaf,
				Mute);
		}

		public static CoreGuildMember From(GuildMember guildMember, User? user = null)
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

			return new CoreGuildMember(ulong.Parse((guildMember.User ?? user!).Id), guildMember.Roles,
				guildMember.Nickname,
				joinedAt, guildMember.Deaf, guildMember.Mute);
		}

		public async Task<CoreUser?> GetUserAsync(Client client)
		{
			return await client.Cache.Users.GetAsync(Id.ToString());
		}
	}
}
