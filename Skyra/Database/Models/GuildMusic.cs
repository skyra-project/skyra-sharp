using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_music")]
	public sealed class GuildMusic
	{
		public uint DefaultVolume { get; set; } = 100;
		public TimeSpan MaximumDuration { get; set; } = TimeSpan.FromHours(1);
		public ushort MaximumEntriesPerUser { get; set; } = 100;
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
