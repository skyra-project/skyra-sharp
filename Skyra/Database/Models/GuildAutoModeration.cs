namespace Skyra.Database.Models
{
	public sealed class GuildAutoModeration
	{
		public ulong Id { get; set; }
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];

		public GuildModerationAttachment Attachment { get; set; } = null!;
		public GuildModerationCapital Capital { get; set; } = null!;
		public GuildModerationLink Link { get; set; } = null!;
		public GuildModerationMessage Message { get; set; } = null!;
		public GuildModerationNewLine NewLine { get; set; } = null!;
		public GuildModerationInvites Invites { get; set; } = null!;
		public GuildModerationWord Word { get; set; } = null!;
		public GuildModerationReaction Reaction { get; set; } = null!;
		public GuildModerationNoMentionSpam NoMentionSpam { get; set; } = null!;

		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;
	}
}
