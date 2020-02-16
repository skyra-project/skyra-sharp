using System;
using System.ComponentModel.DataAnnotations;

namespace Skyra.Database.Models
{
	public class Giveaway
	{
		[MaxLength(256)]
		public string Title { get; set; }

		public DateTime EndsAt { get; set; }
		public ulong GuildId { get; set; }
		public ulong ChannelId { get; set; }
		public ulong MessageId { get; set; }
		public uint MinimumParticipants { get; set; }
		public uint MinimumWinners { get; set; }
	}
}
