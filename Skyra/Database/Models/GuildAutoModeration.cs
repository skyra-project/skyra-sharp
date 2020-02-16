namespace Skyra.Database.Models
{
	public sealed class GuildAutoModeration
	{
		public ulong Id { get; set; }
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];

		public GuildModerationAttachment Attachment { get; set; }
		public GuildModerationCapital Capital { get; set; }
		public GuildModerationLink Link { get; set; }
		public GuildModerationMessage Message { get; set; }
		public GuildModerationNewLine NewLine { get; set; }
		public GuildModerationInvites Invites { get; set; }
		public GuildModerationWord Word { get; set; }
		public GuildModerationReaction Reaction { get; set; }
		public GuildModerationNoMentionSpam NoMentionSpam { get; set; }

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}
