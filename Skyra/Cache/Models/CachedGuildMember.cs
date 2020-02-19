using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Cache.Models
{
	public class CachedGuildMember
	{
		public CachedGuildMember(GuildMember guildMember)
		{
			Id = guildMember.User?.Id;
			Roles = guildMember.Roles;
			Nickname = guildMember.Nickname;
			Deaf = guildMember.Deaf;
			Mute = guildMember.Mute;

			if (DateTime.TryParse(guildMember.JoinedAt, out var result)) JoinedAt = result;
			else JoinedAt = null;
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
	}
}
