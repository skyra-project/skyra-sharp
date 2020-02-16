using System;

namespace Skyra.Database.Models
{
	public class GuildMusic
	{
		public uint DefaultVolume { get; set; } = 100;
		public TimeSpan MaximumDuration { get; set; } = TimeSpan.FromHours(1);
		public ushort MaximumEntriesPerUser { get; set; } = 100;
		public bool AllowStreams { get; set; } = false;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}
}
