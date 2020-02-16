namespace Skyra.Database.Models
{
	public class GuildModerationReaction : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 10;
		public string[] WhiteList { get; set; } = new string[0];
		public string[] BlackList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}