namespace Skyra.Database.Models
{
	public class GuildModerationNoMentionSpam : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 10;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}