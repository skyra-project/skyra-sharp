using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	[Table("guilds")]
	public sealed class Guild
	{
		[Column("id")]
		public ulong Id { get; set; }

		[Column("prefix")]
		public string Prefix { get; set; } = "s!";

		[Column("disable_natural_prefix")]
		public bool DisableNaturalPrefix { get; set; } = false;

		[Column("language")]
		public string Language { get; set; } = "en-US";

		[Column("command_uses")]
		public uint CommandUses { get; set; } = 0;

		[Column("disabled_commands")]
		public string[] DisabledCommands { get; set; } = new string[0];

		/// <summary>
		///     The sticky roles for this guild.
		/// </summary>
		[Column("sticky_roles", TypeName = "JSON[]")]
		public string[] StickyRolesString
		{
			get => StickyRoles.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => StickyRoles = value.Select(JsonConvert.DeserializeObject<GuildStickyRole>).ToArray();
		}

		[NotMapped]
		public GuildStickyRole[] StickyRoles { get; set; } = new GuildStickyRole[0];

		/// <summary>
		///     The actions for this guild.
		/// </summary>
		[Column("actions", TypeName = "JSON[]")]
		public string[] ActionsString
		{
			get => Actions.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => Actions = value.Select(JsonConvert.DeserializeObject<GuildAction>).ToArray();
		}

		[NotMapped]
		public GuildAction[] Actions { get; set; } = new GuildAction[0];

		/// <summary>
		///     The per-channel command auto delete overrides.
		/// </summary>
		[Column("command_auto_delete", TypeName = "JSON[]")]
		public string[] CommandAutoDeleteString
		{
			get => CommandAutoDelete.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => CommandAutoDelete = value.Select(JsonConvert.DeserializeObject<GuildCommandAutoDelete>).ToArray();
		}

		[NotMapped]
		public GuildCommandAutoDelete[] CommandAutoDelete { get; set; } = new GuildCommandAutoDelete[0];

		/// <summary>
		///     The per-channel command blacklist overrides.
		/// </summary>
		[Column("disabled_command_channels", TypeName = "JSON[]")]
		public string[] DisabledCommandChannelsString
		{
			get => DisabledCommandChannels.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => DisabledCommandChannels =
				value.Select(JsonConvert.DeserializeObject<GuildDisabledCommandChannels>).ToArray();
		}

		[NotMapped]
		public GuildDisabledCommandChannels[] DisabledCommandChannels { get; set; } =
			new GuildDisabledCommandChannels[0];

		/// <summary>
		///     The guild's tags.
		/// </summary>
		[Column("tags", TypeName = "JSON[]")]
		public string[] TagsString
		{
			get => Tags.Select(e => JsonConvert.SerializeObject(e)).ToArray();
			set => Tags = value.Select(JsonConvert.DeserializeObject<GuildTag>).ToArray();
		}

		[NotMapped]
		public GuildTag[] Tags { get; set; } = new GuildTag[0];

		public GuildPermission Permission { get; set; } = null!;
		public GuildChannel Channel { get; set; } = null!;
		public GuildEvent Event { get; set; } = null!;
		public GuildMessage Message { get; set; } = null!;
		public GuildRole Role { get; set; } = null!;
		public GuildAutoModeration AutoModeration { get; set; } = null!;
		public GuildSocial Social { get; set; } = null!;
		public GuildMusic Music { get; set; } = null!;
	}
}
