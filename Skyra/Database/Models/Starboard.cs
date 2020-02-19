using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("starboards")]
	public sealed class Starboard
	{
		/// <summary>
		///     Whether or not this starboard entry is enabled.
		/// </summary>
		[Column("enabled")]
		public bool Enabled { get; set; } = true;

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.User" />'s ID that authors this entry.
		/// </summary>
		[Column("user_id")]
		public ulong UserId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Message" />'s ID this entry is bound to.
		/// </summary>
		[Column("message_id")]
		public ulong MessageId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />'s ID this entry is bound to.
		/// </summary>
		[Column("channel_id")]
		public ulong ChannelId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Guild" />'s ID this entry is bound to.
		/// </summary>
		[Column("guild_id")]
		public ulong GuildId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Message" />'s ID this entry will edit when updating the stars.
		/// </summary>
		[Column("star_message_id")]
		public ulong? StarMessageId { get; set; } = null;

		/// <summary>
		///     The amount of stars this entry has.
		/// </summary>
		[Column("stars")]
		public int Stars { get; set; } = 0;
	}
}
