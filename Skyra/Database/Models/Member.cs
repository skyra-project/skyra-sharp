using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("members")]
	public sealed class Member
	{
		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Guild" />'s ID this entry belongs to.
		/// </summary>
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.User" />'s ID this entry belongs to.
		/// </summary>
		[Column("user_id")]
		public ulong UserId { get; set; }

		/// <summary>
		///     The amount of points this member has.
		/// </summary>
		[Column("points")]
		public long Points { get; set; } = 0;

		/// <summary>
		///     The navigation property to the <see cref="User" /> entity.
		/// </summary>
		public User User { get; set; } = null!;
	}
}
