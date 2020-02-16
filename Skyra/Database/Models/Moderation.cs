using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	public sealed class Moderation
	{
		public uint CaseId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Column(TypeName = "JSON")]
		public string ExtraData { get; set; }

		public ulong GuildId { get; set; }
		public ulong ModeratorId { get; set; }
		public ulong UserId { get; set; }
		public short Type { get; set; }
		public string? Reason { get; set; } = null;
		public TimeSpan? Duration { get; set; } = null;
	}
}
