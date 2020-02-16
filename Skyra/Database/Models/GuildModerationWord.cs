namespace Skyra.Database.Models
{
	public sealed class GuildModerationWord : GuildModerationBase
	{
		public string[] Words { get; set; } = new string[0];
	}
}
