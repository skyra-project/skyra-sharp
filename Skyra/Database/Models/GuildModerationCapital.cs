namespace Skyra.Database.Models
{
	public class GuildModerationCapital : GuildModerationBase
	{
		public ushort MinimumCharacters { get; set; } = 10;
		public ushort PercentageCharacters { get; set; } = 50;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}