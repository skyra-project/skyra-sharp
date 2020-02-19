using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_events")]
	public sealed class GuildEvent
	{
		/// <summary>
		///     Whether or not new bans should be logged to <see cref="GuildChannel.ModerationLogs" />.
		/// </summary>
		[Column("ban_add")]
		public bool BanAdd { get; set; } = false;

		/// <summary>
		///     Whether or not revoked bans should be logged to <see cref="GuildChannel.ModerationLogs" />.
		/// </summary>
		[Column("ban_remove")]
		public bool BanRemove { get; set; } = false;

		/// <summary>
		///     Whether or not to log in <see cref="GuildChannel.MemberLogs" /> when a new user joins.
		/// </summary>
		[Column("member_add")]
		public bool MemberAdd { get; set; } = false;

		/// <summary>
		///     Whether or not to log in <see cref="GuildChannel.MemberLogs" /> when a user leaves.
		/// </summary>
		[Column("member_remove")]
		public bool MemberRemove { get; set; } = false;

		/// <summary>
		///     Whether or not to log in <see cref="GuildChannel.MemberLogs" /> when a user changes their nickname or username.
		/// </summary>
		[Column("member_nickname_update")]
		public bool MemberNicknameUpdate { get; set; } = false;

		/// <summary>
		///     Whether or not to log in <see cref="GuildChannel.MessageLogs" /> or <see cref="GuildChannel.NsfwMessageLogs" />
		///     when a message is deleted.
		/// </summary>
		[Column("message_delete")]
		public bool MessageDelete { get; set; } = false;

		/// <summary>
		///     Whether or not to log in <see cref="GuildChannel.MessageLogs" /> or <see cref="GuildChannel.NsfwMessageLogs" />
		///     when a message is edited.
		/// </summary>
		[Column("message_edit")]
		public bool MessageEdit { get; set; } = false;

		/// <summary>
		///     Whether or not unicode emojis (Twemoji) should be logged in <see cref="GuildChannel.ReactionLogs" />.
		/// </summary>
		[Column("twemoji")]
		public bool Twemoji { get; set; } = false;

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
