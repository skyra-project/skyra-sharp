using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_moderation_messages")]
	public sealed class GuildModerationMessage : GuildModerationBase
	{
		public ushort QueueSize { get; set; } = 50;
		public ushort MaximumDuplicates { get; set; } = 5;
	}
}
