namespace Skyra.Database.Models
{
	public sealed class GuildModerationNoMentionSpam : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 10;
	}
}
