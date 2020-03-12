using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildRole : ICoreBaseStructure<CoreGuildRole>
	{
		public CoreGuildRole(ulong id, string name, uint color, bool managed, bool mentionable, Permission permissions,
			uint position)
		{
			Id = id;
			Name = name;
			Color = color;
			Managed = managed;
			Mentionable = mentionable;
			Permissions = permissions;
			Position = position;
		}

		[JsonProperty("id")]
		public ulong Id { get; set; }

		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonProperty("c")]
		public uint Color { get; set; }

		[JsonProperty("md")]
		public bool Managed { get; set; }

		[JsonProperty("me")]
		public bool Mentionable { get; set; }

		[JsonProperty("ps")]
		public Permission Permissions { get; set; }

		[JsonProperty("pt")]
		public uint Position { get; set; }

		public CoreGuildRole Patch(CoreGuildRole value)
		{
			Name = value.Name;
			Color = value.Color;
			Mentionable = value.Mentionable;
			Permissions = value.Permissions;
			Position = value.Position;
			return this;
		}

		public CoreGuildRole Clone()
		{
			return new CoreGuildRole(Id,
				Name,
				Color,
				Managed,
				Mentionable,
				Permissions,
				Position);
		}

		public static CoreGuildRole From(Role role)
		{
			return new CoreGuildRole(ulong.Parse(role.Id), role.Name, (uint) role.Color, role.Managed, role.Mentionable,
				(Permission) role.Permissions, (uint) role.Position);
		}
	}
}
