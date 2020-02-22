using System;

namespace Skyra.Core.Database.Models
{
	[Flags]
	public enum GuildModerationSoftAction : byte
	{
		/// <summary>
		///     No action taken, this represents a disabled moderation action.
		/// </summary>
		None = 0,

		/// <summary>
		///     Whether or not the soft action should issue a warning on the channel
		/// </summary>
		Alert = 1,

		/// <summary>
		///     Whether or not the infraction should be logged in <see cref="GuildChannel.ModerationLogs" />.
		/// </summary>
		Log = 1 << 1,

		/// <summary>
		///     Whether or not the message should be deleted
		/// </summary>
		Delete = 1 << 2
	}
}
