using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CachedGuild
	{
		public CachedGuild(Guild guild)
		{
			Id = guild.Id;
			Name = guild.Name;
			Region = guild.Region;
			Icon = guild.Icon;
			Permissions = guild.Permissions;
			MemberCount = guild.MemberCount;
			OwnerId = guild.OwnerId;
		}

		[JsonConstructor]
		public CachedGuild(string id, string name, string region, string? icon, Permission? permissions,
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
		public string Id { get; set; }

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
	}
}
