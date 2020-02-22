using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Core.Database.Models
{
	[Table("guild_moderation_words")]
	public sealed class GuildModerationWord : GuildModerationBase
	{
		/// <summary>
		///     The filtered words.
		/// </summary>
		[Column("words")]
		[MaxLength(128)]
		public string[] Words { get; set; } = new string[0];
	}
}
