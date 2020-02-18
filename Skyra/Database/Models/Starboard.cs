using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("starboards")]
	public sealed class Starboard
	{
		public bool Enabled { get; set; } = true;
		public ulong UserId { get; set; }
		public ulong MessageId { get; set; }
		public ulong ChannelId { get; set; }
		public ulong GuildId { get; set; }
		public ulong? StarMessageId { get; set; } = null;
		public int Stars { get; set; } = 0;
	}
}
