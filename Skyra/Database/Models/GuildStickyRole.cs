using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildStickyRole
	{
		public GuildStickyRole(ulong user, ulong[] roles)
		{
			User = user;
			Roles = roles;
		}

		public ulong User { get; set; }
		public ulong[] Roles { get; set; }
	}
}
