using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class GuildRole : IBaseStructure<GuildRole>
	{
		public GuildRole(IClient client, ulong id, string name, uint color, bool managed, bool mentionable,
			Permission permissions, uint position)
		{
			Client = client;
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

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public GuildRole Patch([NotNull] GuildRole value)
		{
			Name = value.Name;
			Color = value.Color;
			Mentionable = value.Mentionable;
			Permissions = value.Permissions;
			Position = value.Position;
			return this;
		}

		[NotNull]
		public GuildRole Clone()
		{
			return new GuildRole(Client,
				Id,
				Name,
				Color,
				Managed,
				Mentionable,
				Permissions,
				Position);
		}

		[NotNull]
		public static GuildRole From(IClient client, [NotNull] Role role)
		{
			return new GuildRole(client, ulong.Parse(role.Id), role.Name, (uint) role.Color, role.Managed,
				role.Mentionable,
				(Permission) role.Permissions, (uint) role.Position);
		}
	}
}
