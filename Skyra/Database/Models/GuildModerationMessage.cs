namespace Skyra.Database.Models
{
	public class GuildModerationMessage : GuildModerationBase
	{
		public ushort QueueSize { get; set; } = 50;
		public ushort MaximumDuplicates { get; set; } = 5;

		public ulong GuildId { get; set; }
		public virtual GuildAutoModeration Guild { get; set; }
	}
}