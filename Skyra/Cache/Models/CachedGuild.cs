using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Cache.Models
{
	public class CachedGuild
	{
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
	}
}
