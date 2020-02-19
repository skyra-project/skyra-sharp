using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_invites")]
	public sealed class GuildModerationInvites : GuildModerationBase
	{
		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Guild" /> IDs to ignore when checking an invite's validity and
		///     guild.
		/// </summary>
		[Column("guild_whitelist")]
		public ulong[] GuildWhiteList { get; set; } = new ulong[0];

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Invite.Code" />s to ignore from this sub-system.
		/// </summary>
		[Column("code_whitelist")]
		public string[] CodeWhiteList { get; set; } = new string[0];
	}
}
