namespace Skyra.Database.Models
{
	public sealed class GuildChannel
	{
		public ulong Id { get; set; }
		public ulong? Announcements { get; set; } = null;
		public ulong? Greetings { get; set; } = null;
		public ulong? Farewell { get; set; } = null;
		public ulong? MemberLogs { get; set; } = null;
		public ulong? ModerationLogs { get; set; } = null;
		public ulong? MessageLogs { get; set; } = null;
		public ulong? NsfwMessageLogs { get; set; } = null;
		public ulong? ImageLogs { get; set; } = null;
		public ulong? PruneLogs { get; set; } = null;
		public ulong? ReactionLogs { get; set; } = null;

		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;
	}
}
