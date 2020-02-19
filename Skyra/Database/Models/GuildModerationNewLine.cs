using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_newlines")]
	public sealed class GuildModerationNewLine : GuildModerationBase
	{
		/// <summary>
		///     The amount of new-lines allowed in messages.
		/// </summary>
		[Column("maximum")]
		public ushort Maximum { get; set; } = 5;
	}
}
