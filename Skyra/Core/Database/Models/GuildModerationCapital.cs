using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Core.Database.Models
{
	[Table("guild_moderation_capitals")]
	public sealed class GuildModerationCapital : GuildModerationBase
	{
		/// <summary>
		///     The amount of characters required before this sub-system kicks off.
		/// </summary>
		[Column("minimum_characters")]
		public ushort MinimumCharacters { get; set; } = 10;

		/// <summary>
		///     The percentage of upper-case letters that there needs to be to flag the message as infracting.
		/// </summary>
		[Column("percentage_characters")]
		public ushort PercentageCharacters { get; set; } = 50;
	}
}
