using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Skyra.Core.Database.Models
{
	[Table("guilds")]
	public sealed class Guild
	{
		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Guild" />'s ID.
		/// </summary>
		[Column("id")]
		public ulong Id { get; set; }

		/// <summary>
		///     The prefix for this server
		/// </summary>
		[Column("prefix")]
		[MaxLength(10)]
		public string Prefix { get; set; } = "s!";

		/// <summary>
		///     Whether or not "Hey Skyra" or any other natural prefix option is enabled.
		/// </summary>
		[Column("disable_natural_prefix")]
		public bool DisableNaturalPrefix { get; set; } = false;

		/// <summary>
		///     The language to use for this guild.
		/// </summary>
		[Column("language")]
		[MaxLength(5)]
		public string Language { get; set; } = "en-US";

		/// <summary>
		///     The amount of uses commands had in this guild.
		/// </summary>
		[Column("command_uses")]
		public uint CommandUses { get; set; } = 0;

		/// <summary>
		///     The commands that are disabled in this guild.
		/// </summary>
		[Column("disabled_commands")]
		public string[] DisabledCommands { get; set; } = new string[0];

		/// <summary>
		///     The sticky roles for this guild.
		/// </summary>
		/// [NotMapped]
		[Column("sticky_roles", TypeName = "JSONB")]
		public GuildStickyRole[] StickyRoles { get; set; } = new GuildStickyRole[0];

		/// <summary>
		///     The actions for this guild.
		/// </summary>
		/// [NotMapped]
		[Column("actions", TypeName = "JSONB")]
		public GuildAction[] Actions { get; set; } = new GuildAction[0];

		/// <summary>
		///     The per-channel command auto delete overrides.
		/// </summary>
		[Column("command_auto_delete", TypeName = "JSONB")]
		public GuildCommandAutoDelete[] CommandAutoDelete { get; set; } = new GuildCommandAutoDelete[0];

		/// <summary>
		///     The per-channel command blacklist overrides.
		/// </summary>
		[Column("disabled_command_channels", TypeName = "JSONB")]
		public GuildDisabledCommandChannels[] DisabledCommandChannels { get; set; } =
			new GuildDisabledCommandChannels[0];

		/// <summary>
		///     The guild's tags.
		/// </summary>
		[Column("tags", TypeName = "JSONB")]
		public GuildTag[] Tags { get; set; } = new GuildTag[0];

		/// <summary>
		///     The navigation property to the <see cref="GuildPermission" /> entity.
		/// </summary>
		public GuildPermission Permission { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildChannel" /> entity.
		/// </summary>
		public GuildChannel Channel { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildEvent" /> entity.
		/// </summary>
		public GuildEvent Event { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildMessage" /> entity.
		/// </summary>
		public GuildMessage Message { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildRole" /> entity.
		/// </summary>
		public GuildRole Role { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildAutoModeration" /> entity.
		/// </summary>
		public GuildAutoModeration AutoModeration { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildSocial" /> entity.
		/// </summary>
		public GuildSocial Social { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildMusic" /> entity.
		/// </summary>
		public GuildMusic Music { get; set; } = null!;

		internal static void OnModelCreating(EntityTypeBuilder<Guild> entity)
		{
			entity.Property(e => e.Actions).HasColumnType("JSONB").HasDefaultValueSql("{}::JSONB").ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Select(JsonConvert.SerializeObject).ToArray(),
					x => x.Select(JsonConvert.DeserializeObject<GuildAction>).ToArray());
			entity.Property(e => e.CommandAutoDelete).HasColumnType("JSONB").HasDefaultValueSql("{}::JSONB")
				.ValueGeneratedOnAdd().HasConversion(
					x => x.Select(JsonConvert.SerializeObject).ToArray(),
					x => x.Select(JsonConvert.DeserializeObject<GuildCommandAutoDelete>).ToArray());
			entity.Property(e => e.DisabledCommandChannels).HasColumnType("JSONB").HasDefaultValueSql("{}::JSONB")
				.ValueGeneratedOnAdd().HasConversion(
					x => x.Select(JsonConvert.SerializeObject).ToArray(),
					x => x.Select(JsonConvert.DeserializeObject<GuildDisabledCommandChannels>).ToArray());
			entity.Property(e => e.StickyRoles).HasColumnType("JSONB").HasDefaultValueSql("{}::JSONB")
				.ValueGeneratedOnAdd().HasConversion(
					x => x.Select(JsonConvert.SerializeObject).ToArray(),
					x => x.Select(JsonConvert.DeserializeObject<GuildStickyRole>).ToArray());
			entity.Property(e => e.Tags).HasColumnType("JSONB").HasDefaultValueSql("{}::JSONB").ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Select(JsonConvert.SerializeObject).ToArray(),
					x => x.Select(JsonConvert.DeserializeObject<GuildTag>).ToArray());
			// entity
			// 	.Ignore(e => e.Actions)
			// 	.Ignore(e => e.CommandAutoDelete)
			// 	.Ignore(e => e.DisabledCommandChannels)
			// 	.Ignore(e => e.StickyRoles)
			// 	.Ignore(e => e.Tags);
		}
	}
}
