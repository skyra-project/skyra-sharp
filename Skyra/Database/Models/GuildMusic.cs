using System;

namespace Skyra.Database.Models
{
	public sealed class GuildMusic
	{
		public ulong Id { get; set; }
		public uint DefaultVolume { get; set; } = 100;
		public TimeSpan MaximumDuration { get; set; } = TimeSpan.FromHours(1);
		public ushort MaximumEntriesPerUser { get; set; } = 100;
		public bool AllowStreams { get; set; } = false;

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}
