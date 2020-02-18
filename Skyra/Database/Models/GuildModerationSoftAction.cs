namespace Skyra.Database.Models
{
	public struct GuildModerationSoftAction
	{
		public byte Bitfield { get; }

		/// <summary>
		///     Whether or not the soft action should issue a warning on the channel.
		/// </summary>
		public bool Alert => (Bitfield & 0x001) == 0x001;

		/// <summary>
		///     Whether or not the infraction should be logged in <see cref="GuildChannel.ModerationLogs" />.
		/// </summary>
		public bool Log => (Bitfield & 0x010) == 0x010;

		/// <summary>
		///     Whether or not the message should be deleted.
		/// </summary>
		public bool Delete => (Bitfield & 0x100) == 0x100;

		/// <summary>
		///     Construct this instance from a bitfield.
		/// </summary>
		/// <param name="bitfield">The bitfield to use.</param>
		public GuildModerationSoftAction(byte bitfield)
		{
			Bitfield = bitfield;
		}
	}
}
