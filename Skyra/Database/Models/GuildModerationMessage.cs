namespace Skyra.Database.Models
{
	public sealed class GuildModerationMessage : GuildModerationBase
	{
		public ushort QueueSize { get; set; } = 50;
		public ushort MaximumDuplicates { get; set; } = 5;
	}
}
