namespace Skyra.Database.Models
{
	public sealed class GuildModerationAttachment : GuildModerationBase
	{
		public ulong GuildId { get; set; }
		public GuildAutoModeration Guild { get; set; }
	}
}
