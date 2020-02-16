namespace Skyra.Database.Models
{
	public class GuildModerationNewLine : GuildModerationBase
	{
		public ushort Maximum { get; set; } = 5;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}