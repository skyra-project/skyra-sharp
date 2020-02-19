using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	[Table("guild_permissions")]
	public sealed class GuildPermission
	{
		/// <summary>
		///     The raw value from and for the database. Use <see cref="Users" />
		/// </summary>
		[Column("users", TypeName = "JSON[]")]
		public string[] UsersRaw
		{
			get => Users.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => Users = value.Select(JsonConvert.DeserializeObject<GuildPermissionNode>).ToArray();
		}

		/// <summary>
		///     The <see cref="GuildPermissionNode" />s for <see cref="Spectacles.NET.Types.User" />s.
		/// </summary>
		[NotMapped]
		public GuildPermissionNode[] Users { get; set; } = new GuildPermissionNode[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="Roles" />
		/// </summary>
		[Column("roles", TypeName = "JSON[]")]
		public string[] RolesRaw
		{
			get => Roles.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => Roles = value.Select(JsonConvert.DeserializeObject<GuildPermissionNode>).ToArray();
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
