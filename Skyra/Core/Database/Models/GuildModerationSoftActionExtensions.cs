namespace Skyra.Core.Database.Models
{
	public static class GuildModerationSoftActionExtensions
	{
		/// <summary>
		///     Gets the value for a flag in this instance.
		/// </summary>
		/// <param name="value">The enum instance.</param>
		/// <param name="flag">The flag to check.</param>
		/// <returns>Whether or not the flag has been set.</returns>
		public static bool HasFlagFast(this GuildModerationSoftAction value, GuildModerationSoftAction flag)
		{
			return (value & flag) != 0;
		}

		/// <summary>
		///     Sets the value for a flag in this instance.
		/// </summary>
		/// <param name="value">The enum instance.</param>
		/// <param name="flag">The flag to mutate.</param>
		/// <param name="enable">Whether to set the values as 0 or as 1.</param>
		public static void SetFlag(ref this GuildModerationSoftAction value, GuildModerationSoftAction flag,
			bool enable)
		{
			value = enable ? value | flag : value & ~flag;
		}
	}
}
