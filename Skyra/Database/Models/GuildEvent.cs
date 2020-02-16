namespace Skyra.Database.Models
{
	public sealed class GuildEvent
	{
		public ulong Id { get; set; }
		public bool BanAdd { get; set; } = false;
		public bool BanRemove { get; set; } = false;
		public bool MemberAdd { get; set; } = false;
		public bool MemberRemove { get; set; } = false;
		public bool MemberNicknameUpdate { get; set; } = false;
		public bool MessageDelete { get; set; } = false;
		public bool MessageEdit { get; set; } = false;
		public bool Twemoji { get; set; } = false;

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}
