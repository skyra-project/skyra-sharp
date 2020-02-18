using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_invites")]
	public sealed class GuildModerationInvites : GuildModerationBase
	{
		public ulong[] GuildWhiteList { get; set; } = new ulong[0];
		public string[] CodeWhiteList { get; set; } = new string[0];
	}
}
