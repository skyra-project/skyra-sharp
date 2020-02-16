using System;

namespace Skyra.Database.Models
{
	public abstract class GuildModerationBase
	{
		public ulong Id { get; set; }
		public bool Enabled { get; set; } = false;
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public GuildModerationSoftAction SoftActionType { get; set; } = GuildModerationSoftAction.None;
		public ushort SoftActionThresholdMaximum { get; set; } = 120;
		public TimeSpan SoftActionThresholdDuration { get; set; } = TimeSpan.FromMinutes(2);
		public GuildModerationHardAction HardActionType { get; set; } = GuildModerationHardAction.None;
		public TimeSpan HardActionDuration { get; set; } = TimeSpan.Zero;
		public ushort HardActionThresholdMaximum { get; set; } = 120;
		public TimeSpan HardActionThresholdDuration { get; set; } = TimeSpan.FromMinutes(2);

		public ulong GuildId { get; set; }
		public GuildAutoModeration Guild { get; set; }
	}
}
