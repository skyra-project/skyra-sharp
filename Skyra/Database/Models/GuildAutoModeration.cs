using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_auto_moderation")]
	public sealed class GuildAutoModeration
	{
		/// <summary>
		///     The roles all subsystems should ignore when running.
		/// </summary>
		[Column("ignored_roles")]
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];

		/// <summary>
		///     The channels all subsystems should ignore when running.
		/// </summary>
		[Column("ignored_channels")]
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationAttachment" /> entity.
		/// </summary>
		public GuildModerationAttachment Attachment { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationCapital" /> entity.
		/// </summary>
		public GuildModerationCapital Capital { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationLink" /> entity.
		/// </summary>
		public GuildModerationLink Link { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationMessage" /> entity.
		/// </summary>
		public GuildModerationMessage Message { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationNewLine" /> entity.
		/// </summary>
		public GuildModerationNewLine NewLine { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationInvites" /> entity.
		/// </summary>
		public GuildModerationInvites Invites { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationWord" /> entity.
		/// </summary>
		public GuildModerationWord Word { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationReaction" /> entity.
		/// </summary>
		public GuildModerationReaction Reaction { get; set; } = null!;

		/// <summary>
		///     The navigation property to the <see cref="GuildModerationNoMentionSpam" /> entity.
		/// </summary>
		public GuildModerationNoMentionSpam NoMentionSpam { get; set; } = null!;

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
