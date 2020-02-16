namespace Skyra.Database.Models
{
	public sealed class GuildModerationNewLine : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 5;
	}
}
