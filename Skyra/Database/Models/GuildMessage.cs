using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_messages")]
	public sealed class GuildMessage
	{
		/// <summary>
		///     The message to be sent in <see cref="GuildChannel.Farewell" /> when a user leaves.
		/// </summary>
		[Column("farewell")]
		public string? Farewell { get; set; } = null;

		/// <summary>
		///     The message to be sent in <see cref="GuildChannel.Greetings" /> when a new user joins.
		/// </summary>
		[Column("greeting")]
		public string? Greeting { get; set; } = null;

		/// <summary>
		///     The message to be sent via DMs when a user joins.
		/// </summary>
		[Column("join_dm")]
		public string? JoinDm { get; set; } = null;

		/// <summary>
		///     Whether or not moderation logs should send a Direct Message to the target user.
		/// </summary>
		[Column("moderation_dm")]
		public bool ModerationDm { get; set; } = false;

		/// <summary>
		///     Whether or not the reason should be displayed in the output message of moderation logs.
		/// </summary>
		[Column("moderation_reason_display")]
		public bool ModerationReasonDisplay { get; set; } = true;

		/// <summary>
		///     Whether or not a message should be sent when issuing a moderation action.
		/// </summary>
		[Column("moderation_message_display")]
		public bool ModerationMessageDisplay { get; set; } = true;

		/// <summary>
		///     Whether or not the moderator's message should be deleted when issuing a moderation action.
		/// </summary>
		[Column("moderation_auto_delete")]
		public bool ModerationAutoDelete { get; set; } = false;

		/// <summary>
		///     Whether or not to display the moderator's name in the Direct Message log.
		/// </summary>
		[Column("moderator_name_display")]
		public bool ModeratorNameDisplay { get; set; } = true;

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
