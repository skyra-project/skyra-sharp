using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_newlines")]
	public sealed class GuildModerationNewLine : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 5;
	}
}
