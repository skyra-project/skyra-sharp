namespace Skyra.Database.Models
{
	public class GuildModerationLink : GuildModerationBase
	{
		public string[] WhiteList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}