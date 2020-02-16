using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildRolesRoleSet
	{
		public string Name { get; set; }
		public ulong[] Roles { get; set; }
	}
}
