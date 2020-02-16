using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildRolesReaction
	{
		public GuildRolesReaction(ulong roleId, ulong channelId, ulong messageId)
		{
			RoleId = roleId;
			ChannelId = channelId;
			MessageId = messageId;
		}

		public ulong RoleId { get; set; }
		public ulong ChannelId { get; set; }
		public ulong MessageId { get; set; }
	}
}
