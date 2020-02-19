using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_channels")]
	public sealed class GuildChannel
	{
		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for announcements.
		/// </summary>
		[Column("announcements")]
		public ulong? Announcements { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for greeting messages.
		/// </summary>
		[Column("greetings")]
		public ulong? Greetings { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for farewell messages.
		/// </summary>
		[Column("farewell")]
		public ulong? Farewell { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for member logs.
		/// </summary>
		[Column("member_logs")]
		public ulong? MemberLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for moderation logs.
		/// </summary>
		[Column("moderation_logs")]
		public ulong? ModerationLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for message logs.
		/// </summary>
		[Column("message_logs")]
		public ulong? MessageLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for NSFW message logs.
		/// </summary>
		[Column("nsfw_message_logs")]
		public ulong? NsfwMessageLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for image logs.
		/// </summary>
		[Column("image_logs")]
		public ulong? ImageLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for prune logs.
		/// </summary>
		[Column("prune_logs")]
		public ulong? PruneLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID for reaction logs.
		/// </summary>
		[Column("reaction_logs")]
		public ulong? ReactionLogs { get; set; } = null;

		/// <summary>
		///     The <see cref="Guild" /> foreign key and primary key for this entity.
		/// </summary>
		[Key]
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;
	}
}
