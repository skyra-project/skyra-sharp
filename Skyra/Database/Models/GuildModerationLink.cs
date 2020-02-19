using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_links")]
	public sealed class GuildModerationLink : GuildModerationBase
	{
		/// <summary>
		///     The links to be whitelisted by this sub-system.
		/// </summary>
		[Column("whitelist")]
		public string[] WhiteList { get; set; } = new string[0];
	}
}
