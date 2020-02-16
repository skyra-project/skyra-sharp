namespace Skyra.Database.Models
{
	public sealed class GuildModerationInvites : GuildModerationBase
	{
		public ulong[] GuildWhiteList { get; set; } = new ulong[0];
		public string[] CodeWhiteList { get; set; } = new string[0];
	}
}
