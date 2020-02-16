namespace Skyra.Database.Models
{
	public class GuildModerationInvites : GuildModerationBase
	{
		public ulong[] GuildWhiteList { get; set; } = new ulong[0];
		public string[] CodeWhiteList { get; set; } = new string[0];

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}