namespace Skyra.Database.Models
{
	public sealed class GuildMessage
	{
		public ulong Id { get; set; }
		public string? Farewell { get; set; } = null;
		public string? Greeting { get; set; } = null;
		public string? JoinDm { get; set; } = null;
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public bool ModerationDm { get; set; } = false;
		public bool ModerationReasonDisplay { get; set; } = true;
		public bool ModerationMessageDisplay { get; set; } = true;
		public bool ModerationAutoDelete { get; set; } = false;
		public bool ModeratorNameDisplay { get; set; } = true;

		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;
	}
}
