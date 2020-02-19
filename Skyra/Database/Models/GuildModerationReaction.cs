using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_reactions")]
	public sealed class GuildModerationReaction : GuildModerationBase
	{
		/// <summary>
		///     The maximum amount of reactions allowed at a certain time.
		/// </summary>
		[Column("maximum")]
		public ushort Maximum { get; set; } = 10;

		/// <summary>
		///     The reactions that should not count towards <see cref="Maximum" />.
		/// </summary>
		[Column("whitelist")]
		public string[] WhiteList { get; set; } = new string[0];

		/// <summary>
		///     The reactions that should be removed by this sub-system and count until <see cref="Maximum" />.
		/// </summary>
		[Column("blacklist")]
		public string[] BlackList { get; set; } = new string[0];
	}
}
