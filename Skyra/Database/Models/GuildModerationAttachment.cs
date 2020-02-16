namespace Skyra.Database.Models
{
	public class GuildModerationAttachment : GuildModerationBase
	{
		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}