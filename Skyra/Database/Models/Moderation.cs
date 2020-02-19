using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("moderations")]
	public sealed class Moderation
	{
		/// <summary>
		///     The case id for this moderation case.
		/// </summary>
		[Column("case_id")]
		public uint CaseId { get; set; }

		/// <summary>
		///     The creation date for this entry.
		/// </summary>
		[Column("created_at")]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		/// <summary>
		///     The guild id this entry belongs to.
		/// </summary>
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.User" /> ID for the moderator.
		/// </summary>
		/// <remarks>
		///     This value can be Skyra herself, this case being on anonymous logs.
		/// </remarks>
		[Column("moderator_id")]
		public ulong ModeratorId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.User" /> ID for the target user.
		/// </summary>
		[Column("user_id")]
		public ulong UserId { get; set; }

		/// <summary>
		///     The type for this entry.
		/// </summary>
		[Column("type")]
		public short Type { get; set; }

		/// <summary>
		///     The extra data, this is a nullable JSON type.
		/// </summary>
		[Column("extra_data", TypeName = "JSON")]
		public string? ExtraData { get; set; } = null;

		/// <summary>
		///     The reason for this moderation log.
		/// </summary>
		[Column("reason")]
		public string? Reason { get; set; } = null;

		/// <summary>
		///     The duration for this moderation log.
		/// </summary>
		[Column("duration")]
		public TimeSpan? Duration { get; set; } = null;

		/// <summary>
		///     The expire time for this moderation log.
		/// </summary>
		/// <remarks>Returns `null` when the <see cref="Duration" /> was not set.</remarks>
		/// <returns>Returns the <see cref="DateTime" /> in which this entry expires at.</returns>
		public DateTime? ExpiresAt()
		{
			return CreatedAt + Duration;
		}
	}
}
