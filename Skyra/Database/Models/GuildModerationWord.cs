using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_words")]
	public sealed class GuildModerationWord : GuildModerationBase
	{
		public string[] Words { get; set; } = new string[0];
	}
}
