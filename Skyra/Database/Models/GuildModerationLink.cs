namespace Skyra.Database.Models
{
	public sealed class GuildModerationLink : GuildModerationBase
	{
		public string[] WhiteList { get; set; } = new string[0];
	}
}
