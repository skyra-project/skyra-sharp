using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_links")]
	public sealed class GuildModerationLink : GuildModerationBase
	{
		public string[] WhiteList { get; set; } = new string[0];
	}
}
