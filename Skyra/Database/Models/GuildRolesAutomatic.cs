using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildRolesAutomatic
	{
		public GuildRolesAutomatic(ulong roleId, ulong points)
		{
			RoleId = roleId;
			Points = points;
		}

		public ulong RoleId { get; set; }
		public ulong Points { get; set; }
	}
}
