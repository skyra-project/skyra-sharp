namespace Skyra.Database.Models
{
	public sealed class Guild
	{
		public ulong Id { get; set; }
		public string Prefix { get; set; } = "s!";
		public bool DisableNaturalPrefix { get; set; } = false;
		public string Language { get; set; } = "en-US";
		public uint CommandUses { get; set; } = 0;
		public string[] DisabledCommands { get; set; } = new string[0];
		public GuildTag[] Tags { get; set; } = new GuildTag[0];
		public GuildStickyRole[] StickyRoles { get; set; } = new GuildStickyRole[0];
		public GuildAction[] Actions { get; set; } = new GuildAction[0];
		public GuildCommandAutoDelete[] CommandAutoDelete { get; set; } = new GuildCommandAutoDelete[0];

		public GuildDisabledCommandChannels[] DisabledCommandChannels { get; set; } =
			new GuildDisabledCommandChannels[0];

		public GuildPermission Permission { get; set; }
		public GuildChannel Channel { get; set; }
		public GuildEvent Event { get; set; }
		public GuildMessage Message { get; set; }
		public GuildRole Role { get; set; }
		public GuildAutoModeration AutoModeration { get; set; }
		public GuildSocial Social { get; set; }
		public GuildMusic Music { get; set; }
	}
}
