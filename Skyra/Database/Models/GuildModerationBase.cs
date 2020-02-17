using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	public abstract class GuildModerationBase
	{
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

		/// <summary>
		///     The <see cref="Guild" /> foreign key and primary key for this entity.
		/// </summary>
		[Key]
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="GuildAutoModeration" /> entity.
		/// </summary>
		public GuildAutoModeration Guild { get; set; } = null!;
	}
}
