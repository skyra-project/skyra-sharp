using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildMember : ICoreBaseStructure<CoreGuildMember>
	{
		public CoreGuildMember(GuildMember guildMember, User? user = null)
		{
			Id = (guildMember.User ?? user!).Id;
			Roles = guildMember.Roles;
			Nickname = guildMember.Nickname;
			Deaf = guildMember.Deaf;
			Mute = guildMember.Mute;

			if (DateTime.TryParse(guildMember.JoinedAt, out var result))
			{
				JoinedAt = result;
			}
			else
			{
				JoinedAt = null;
			}
		}

		[JsonConstructor]
		public CoreGuildMember(string id, List<string> roles, string? nickname, DateTime? joinedAt, bool deaf,
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
		public string Id { get; set; }

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

		public async Task<CoreUser?> GetUserAsync(Client client)
		{
			return await client.Cache.Users.GetAsync(Id);
		}
	}
}
