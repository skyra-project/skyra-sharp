using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	public sealed class Giveaway
	{
		/// <summary>
		///     The title for this giveaway entry.
		/// </summary>
		[Column("title")]
		[MaxLength(256)]
		public string Title { get; set; } = null!;

		/// <summary>
		///     The time in which this giveaway ends.
		/// </summary>
		[Column("ends_at")]
		public DateTime EndsAt { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Guild" /> in which this giveaway is run at.
		/// </summary>
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" /> in which this giveaway is run at.
		/// </summary>
		[Column("channel_id")]
		public ulong ChannelId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Message" /> which displays this giveaway.
		/// </summary>
		[Column("message_id")]
		public ulong MessageId { get; set; }

		/// <summary>
		///     The minimum amount of participants this giveaway needs in order to search for winners.
		/// </summary>
		[Column("minimum_participants")]
		public uint MinimumParticipants { get; set; } = 1;

		/// <summary>
		///     The minimum amount of winners that will be picked from the giveaway.
		/// </summary>
		[Column("minimum_winners")]
		public uint MinimumWinners { get; set; } = 1;
	}
}
