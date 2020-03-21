using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreGuild : ICoreBaseStructure<CoreGuild>
	{
		public CoreGuild(IClient client, ulong id, string name, string region, string? icon, Permission? permissions,
			int? memberCount, string ownerId)
		{
			Client = client;
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

		[JsonIgnore]
		public IClient Client { get; }

		public CoreGuild Patch(CoreGuild value)
		{
			Name = value.Name;
			Region = value.Region;
			Icon = value.Icon;
			Permissions = value.Permissions;
			MemberCount = value.MemberCount;
			OwnerId = value.OwnerId;
			return this;
		}

		public CoreGuild Clone()
		{
			return new CoreGuild(Client,
				Id,
				Name,
				Region,
				Icon,
				Permissions,
				MemberCount,
				OwnerId);
		}

		public async Task<CoreGuildChannel[]> GetChannelsAsync()
		{
			return await Client.Cache.GuildChannels.GetAllAsync(Id.ToString());
		}

		public async Task<CoreGuildRole[]> GetRolesAsync()
		{
			return await Client.Cache.GuildRoles.GetAllAsync(Id.ToString());
		}

		public async Task<CoreGuildMember[]> GetMembersAsync()
		{
			return await Client.Cache.GuildMembers.GetAllAsync(Id.ToString());
		}

		public static CoreGuild From(IClient client, Guild guild)
		{
			return new CoreGuild(client, ulong.Parse(guild.Id), guild.Name, guild.Region, guild.Icon, guild.Permissions,
				guild.MemberCount, guild.OwnerId);
		}
	}
}
