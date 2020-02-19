using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	public abstract class GuildModerationBase
	{
		/// <summary>
		///     Whether this sub-system is enabled or not.
		/// </summary>
		[Column("enabled")]
		public bool Enabled { get; set; } = false;

		/// <summary>
		///     The roles this sub-system should ignore when running.
		/// </summary>
		[Column("ignored_roles")]
		public ulong[] IgnoredRoles { get; set; } = new ulong[0];

		/// <summary>
		///     The channels this sub-system should ignore when running.
		/// </summary>
		[Column("ignored_channels")]
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];

		/// <summary>
		///     The bitfield for the actions to take on every infraction.
		/// </summary>
		[Column("soft_action_type", TypeName = "NUMERIC(1)")]
		public GuildModerationSoftAction SoftActionType { get; set; } = GuildModerationSoftAction.None;

		/// <summary>
		///     The maximum accumulative infractions a user can have before a soft action is issued.
		/// </summary>
		[Column("soft_action_threshold_maximum")]
		public ushort SoftActionThresholdMaximum { get; set; } = 1;

		/// <summary>
		///     The duration at which the accumulated infractions are valid.
		/// </summary>
		[Column("soft_action_threshold_duration")]
		public TimeSpan SoftActionThresholdDuration { get; set; } = TimeSpan.Zero;

		/// <summary>
		///     The action to take when the thresholds have been reached.
		/// </summary>
		[Column("hard_action_type")]
		public GuildModerationHardAction HardActionType { get; set; } = GuildModerationHardAction.None;

		/// <summary>
		///     The duration to set to the hard action.
		/// </summary>
		[Column("hard_action_duration")]
		public TimeSpan HardActionDuration { get; set; } = TimeSpan.Zero;

		/// <summary>
		///     The maximum accumulative infractions required for the hard action to take action.
		/// </summary>
		[Column("hard_action_threshold_maximum")]
		public ushort HardActionThresholdMaximum { get; set; } = 120;

		/// <summary>
		///     The duration at which the accumulated infractions are valid.
		/// </summary>
		[Column("hard_action_threshold_duration")]
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
