using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Core.Database.Models
{
	[Table("guild_moderation_messages")]
	public sealed class GuildModerationMessage : GuildModerationBase
	{
		/// <summary>
		///     The queue size for this sub-system to check duplicates.
		/// </summary>
		[Column("queue_size")]
		public ushort QueueSize { get; set; } = 50;

		/// <summary>
		///     The maximum of duplicates this sub-system should have.
		/// </summary>
		[Column("maximum_duplicates")]
		public ushort MaximumDuplicates { get; set; } = 5;
	}
}
