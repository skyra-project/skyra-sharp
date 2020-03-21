using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreGuildMember : ICoreBaseStructure<CoreGuildMember>
	{
		public CoreGuildMember(IClient client, ulong id, ulong[] roles, string? nickname, DateTime? joinedAt, bool deaf,
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
		public IClient Client { get; }

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
			return new CoreGuildMember(Client,
				Id,
				Roles,
				Nickname,
				JoinedAt,
				Deaf,
				Mute);
		}

		public async Task<CoreUser?> GetUserAsync()
		{
			return await Client.Cache.Users.GetAsync(Id.ToString());
		}

		public static CoreGuildMember From(IClient client, GuildMember guildMember, User? user = null)
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

			return new CoreGuildMember(client, ulong.Parse((guildMember.User ?? user!).Id),
				guildMember.Roles.Select(ulong.Parse).ToArray(),
				guildMember.Nickname, joinedAt, guildMember.Deaf, guildMember.Mute);
		}
	}
}
