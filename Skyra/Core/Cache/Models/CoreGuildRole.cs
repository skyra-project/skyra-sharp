using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildRole : ICoreBaseStructure<CoreGuildRole>
	{
		public CoreGuildRole(Role role)
		{
			Id = ulong.Parse(role.Id);
			Name = role.Name;
			Color = (uint) role.Color;
			Managed = role.Managed;
			Mentionable = role.Mentionable;
			Permissions = (Permission) role.Permissions;
			Position = (uint) role.Position;
		}

		[JsonConstructor]
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

		public void Patch(CoreGuildRole value)
		{
			Name = value.Name;
			Color = value.Color;
			Mentionable = value.Mentionable;
			Permissions = value.Permissions;
			Position = value.Position;
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
	}
}
