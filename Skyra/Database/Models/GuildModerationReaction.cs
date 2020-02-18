using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_reactions")]
	public sealed class GuildModerationReaction : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 10;
		public string[] WhiteList { get; set; } = new string[0];
		public string[] BlackList { get; set; } = new string[0];
	}
}
