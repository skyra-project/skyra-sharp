using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildRolesReaction
	{
		public ulong RoleId { get; set; }
		public ulong ChannelId { get; set; }
		public ulong MessageId { get; set; }
	}
}