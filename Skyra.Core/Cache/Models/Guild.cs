using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class Guild : IBaseStructure<Guild>
	{
		public Guild(IClient client, ulong id, string name, string region, string? icon, Permission? permissions,
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
		public IClient Client { get; set; }

		[NotNull]
		public Guild Patch([NotNull] Guild value)
		{
			Name = value.Name;
			Region = value.Region;
			Icon = value.Icon;
			Permissions = value.Permissions;
			MemberCount = value.MemberCount;
			OwnerId = value.OwnerId;
			return this;
		}

		[NotNull]
		public Guild Clone()
		{
			return new Guild(Client,
				Id,
				Name,
				Region,
				Icon,
				Permissions,
				MemberCount,
				OwnerId);
		}

		[ItemNotNull]
		public async Task<GuildChannel[]> GetChannelsAsync()
		{
			return await Client.Cache.GuildChannels.GetAllAsync(Id.ToString());
		}

		[ItemNotNull]
		public async Task<GuildRole[]> GetRolesAsync()
		{
			return await Client.Cache.GuildRoles.GetAllAsync(Id.ToString());
		}

		[ItemNotNull]
		public async Task<GuildMember[]> GetMembersAsync()
		{
			return await Client.Cache.GuildMembers.GetAllAsync(Id.ToString());
		}

		[NotNull]
		public static Guild From(IClient client, [NotNull] Spectacles.NET.Types.Guild guild)
		{
			return new Guild(client, ulong.Parse(guild.Id), guild.Name, guild.Region, guild.Icon, guild.Permissions,
				guild.MemberCount, guild.OwnerId);
		}
	}
}
