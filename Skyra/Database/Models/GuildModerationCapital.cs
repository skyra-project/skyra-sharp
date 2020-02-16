namespace Skyra.Database.Models
{
	public sealed class GuildModerationCapital : GuildModerationBase
	{
		public ushort MinimumCharacters { get; set; } = 10;
		public ushort PercentageCharacters { get; set; } = 50;
	}
}
