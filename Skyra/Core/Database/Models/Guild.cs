using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

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
		///     The raw value from and for the database. Use <see cref="StickyRoles" />
		/// </summary>
		[Column("sticky_roles", TypeName = "JSON[]")]
		public string[] StickyRolesRaw
		{
			get => StickyRoles.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => StickyRoles = value.Select(JsonConvert.DeserializeObject<GuildStickyRole>).ToArray();
		}

		/// <summary>
		///     The sticky roles for this guild.
		/// </summary>
		[NotMapped]
		public GuildStickyRole[] StickyRoles { get; set; } = new GuildStickyRole[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="Actions" />
		/// </summary>
		[Column("actions", TypeName = "JSON[]")]
		public string[] ActionsRaw
		{
			get => Actions.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => Actions = value.Select(JsonConvert.DeserializeObject<GuildAction>).ToArray();
		}

		/// <summary>
		///     The actions for this guild.
		/// </summary>
		[NotMapped]
		public GuildAction[] Actions { get; set; } = new GuildAction[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="CommandAutoDelete" />
		/// </summary>
		[Column("command_auto_delete", TypeName = "JSON[]")]
		public string[] CommandAutoDeleteRaw
		{
			get => CommandAutoDelete.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => CommandAutoDelete = value.Select(JsonConvert.DeserializeObject<GuildCommandAutoDelete>).ToArray();
		}

		/// <summary>
		///     The per-channel command auto delete overrides.
		/// </summary>
		[NotMapped]
		public GuildCommandAutoDelete[] CommandAutoDelete { get; set; } = new GuildCommandAutoDelete[0];

		/// <summary>
		///     The raw value from and for the database. Use <see cref="DisabledCommandChannels" />
		/// </summary>
		[Column("disabled_command_channels", TypeName = "JSON[]")]
		public string[] DisabledCommandChannelsRaw
		{
			get => DisabledCommandChannels.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => DisabledCommandChannels =
				value.Select(JsonConvert.DeserializeObject<GuildDisabledCommandChannels>).ToArray();
		}

		/// <summary>
		///     The per-channel command blacklist overrides.
		/// </summary>
		[NotMapped]
		public GuildDisabledCommandChannels[] DisabledCommandChannels { get; set; } =
			new GuildDisabledCommandChannels[0];


		/// <summary>
		///     The raw value from and for the database. Use <see cref="Tags" />
		/// </summary>
		[Column("tags", TypeName = "JSON[]")]
		public string[] TagsRaw
		{
			get => Tags.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => Tags = value.Select(JsonConvert.DeserializeObject<GuildTag>).ToArray();
		}

		/// <summary>
		///     The guild's tags.
		/// </summary>
		[NotMapped]
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
	}
}
