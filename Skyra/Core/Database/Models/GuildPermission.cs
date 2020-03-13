using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Skyra.Core.Database.Models
{
	[Table("guild_permissions")]
	public sealed class GuildPermission
	{
		/// <summary>
		///     The raw value from and for the database. Use <see cref="Users" />
		/// </summary>
		[Column("users", TypeName = "JSONB")]
		public string UsersRaw
		{
			get => JsonConvert.SerializeObject(Users);
			set => Users = JsonConvert.DeserializeObject<GuildPermissionNode[]>(value);
		}

		/// <summary>
		///     The <see cref="GuildPermissionNode" />s for <see cref="Spectacles.NET.Types.User" />s.
		/// </summary>
		[NotMapped]
		public GuildPermissionNode[] Users { get; set; } = new GuildPermissionNode[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="Roles" />
		/// </summary>
		[Column("roles", TypeName = "JSONB")]
		public string RolesRaw
		{
			get => JsonConvert.SerializeObject(Roles);
			set => Roles = JsonConvert.DeserializeObject<GuildPermissionNode[]>(value);
		}

		/// <summary>
		///     The <see cref="GuildPermissionNode" />s for <see cref="Spectacles.NET.Types.User" />s.
		/// </summary>
		[NotMapped]
		public GuildPermissionNode[] Roles { get; set; } = new GuildPermissionNode[0];

		/// <summary>
		///     The <see cref="Guild" /> foreign key and primary key for this entity.
		/// </summary>
		[Key]
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;
	}
}
