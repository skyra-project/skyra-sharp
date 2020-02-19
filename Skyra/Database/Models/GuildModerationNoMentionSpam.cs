using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_no_mention_spam")]
	public sealed class GuildModerationNoMentionSpam : GuildModerationBase
	{
		/// <summary>
		///     The maximum amount of mentions allowed by this sub-system.
		/// </summary>
		[Column("maximum")]
		public ushort Maximum { get; set; } = 10;
	}
}
