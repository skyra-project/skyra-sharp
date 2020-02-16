namespace Skyra.Database.Models
{
	public class Member
	{
		public ulong GuildId { get; set; }
		public ulong UserId { get; set; }
		public long PointCount { get; set; } = 0;

		public virtual User User { get; set; }
	}
}
