using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_music")]
	public sealed class GuildMusic
	{
		/// <summary>
		///     The default volume to be set in Lavalink when a new song starts playing.
		/// </summary>
		[Column("default_volume")]
		public uint DefaultVolume { get; set; } = 100;

		/// <summary>
		///     The maximum duration allowed to be queued.
		/// </summary>
		[Column("maximum_duration")]
		public TimeSpan MaximumDuration { get; set; } = TimeSpan.FromHours(1);

		/// <summary>
		///     The maximum amount of songs a user can queue.
		/// </summary>
		[Column("maximum_entries_per_user")]
		public ushort MaximumEntriesPerUser { get; set; } = 100;

		/// <summary>
		///     Whether or not this sub-system should allow streams to be queued.
		/// </summary>
		[Column("allow_streams")]
		public bool AllowStreams { get; set; } = false;

		/// <summary>
		///     The <see cref="Guild" /> foreign key and primary key for this entity.
		/// </summary>
		[Key]
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The navigation property to the <see cref="Guild" /> entity.
		/// </summary>
		public Guild Guild { get; set; } = null!;
	}
}
