using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("guild_events")]
	public sealed class GuildEvent
	{
		public bool BanAdd { get; set; } = false;
		public bool BanRemove { get; set; } = false;
		public bool MemberAdd { get; set; } = false;
		public bool MemberRemove { get; set; } = false;
		public bool MemberNicknameUpdate { get; set; } = false;
		public bool MessageDelete { get; set; } = false;
		public bool MessageEdit { get; set; } = false;
		public bool Twemoji { get; set; } = false;

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
