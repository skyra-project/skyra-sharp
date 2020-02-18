using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_capitals")]
	public sealed class GuildModerationCapital : GuildModerationBase
	{
		public ushort MinimumCharacters { get; set; } = 10;
		public ushort PercentageCharacters { get; set; } = 50;
	}
}
