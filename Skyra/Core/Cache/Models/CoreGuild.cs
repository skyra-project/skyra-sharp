using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuild : ICoreBaseStructure<CoreGuild>
	{
		public CoreGuild(Guild guild)
		{
			Id = ulong.Parse(guild.Id);
			Name = guild.Name;
			Region = guild.Region;
			Icon = guild.Icon;
			Permissions = guild.Permissions;
			MemberCount = guild.MemberCount;
			OwnerId = guild.OwnerId;
		}

		[JsonConstructor]
		public CoreGuild(ulong id, string name, string region, string? icon, Permission? permissions,
			int? memberCount, string ownerId)
		{
			Id = id;
			Name = name;
			Region = region;
			Icon = icon;
			Permissions = permissions;
			MemberCount = memberCount;
			OwnerId = ownerId;
		}

		[JsonProperty("id")]
		public ulong Id { get; set; }

		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonProperty("r")]
		public string Region { get; set; }

		[JsonProperty("i")]
		public string? Icon { get; set; }

		[JsonProperty("p")]
		public Permission? Permissions { get; set; }

		[JsonProperty("m")]
		public int? MemberCount { get; set; }

		[JsonProperty("o")]
		public string OwnerId { get; set; }

		public void Patch(CoreGuild value)
		{
			Name = value.Name;
			Region = value.Region;
			Icon = value.Icon;
			Permissions = value.Permissions;
			MemberCount = value.MemberCount;
			OwnerId = value.OwnerId;
		}

		public CoreGuild Clone()
		{
			return new CoreGuild(Id,
				Name,
				Region,
				Icon,
				Permissions,
				MemberCount,
				OwnerId);
		}

		public async Task<CoreGuildChannel[]> GetChannelsAsync(Client client)
		{
			return await client.Cache.GuildChannels.GetAllAsync(Id.ToString());
		}

		public async Task<CoreGuildRole[]> GetRolesAsync(Client client)
		{
			return await client.Cache.GuildRoles.GetAllAsync(Id.ToString());
		}

		public async Task<CoreGuildMember[]> GetMembersAsync(Client client)
		{
			return await client.Cache.GuildMembers.GetAllAsync(Id.ToString());
		}
	}
}