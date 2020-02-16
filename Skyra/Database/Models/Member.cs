namespace Skyra.Database.Models
{
	public sealed class Member
	{
		public ulong GuildId { get; set; }
		public ulong UserId { get; set; }
		public long PointCount { get; set; } = 0;

		public User User { get; set; }
	}
}
