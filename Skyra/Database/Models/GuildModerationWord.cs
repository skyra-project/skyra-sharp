namespace Skyra.Database.Models
{
	public class GuildModerationWord : GuildModerationBase
	{
		public string[] Words { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}