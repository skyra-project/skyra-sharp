namespace Skyra.Database.Models
{
	public struct GuildModerationSoftAction
	{
		/// <summary>
		///     The raw bitfield number
		/// </summary>
		public byte Bitfield { get; private set; }

		/// <summary>
		///     Whether or not the soft action should issue a warning on the channel.
		/// </summary>
		public bool Alert
		{
			get => (Bitfield & 0b001) == 0b001;
			set => Bitfield = (byte) (value ? Bitfield | 0b001 : Bitfield & 0b110);
		}

		/// <summary>
		///     Whether or not the infraction should be logged in <see cref="GuildChannel.ModerationLogs" />.
		/// </summary>
		public bool Log
		{
			get => (Bitfield & 0b010) == 0b010;
			set => Bitfield = (byte) (value ? Bitfield | 0b010 : Bitfield & 0b101);
		}

		/// <summary>
		///     Whether or not the message should be deleted.
		/// </summary>
		public bool Delete
		{
			get => (Bitfield & 0b100) == 0b100;
			set => Bitfield = (byte) (value ? Bitfield | 0b100 : Bitfield & 0b011);
		}

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
