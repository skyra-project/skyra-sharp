using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildRolesRoleSet
	{
		public GuildRolesRoleSet(string name, ulong[] roles)
		{
			Name = name;
			Roles = roles;
		}

		public string Name { get; set; }
		public ulong[] Roles { get; set; }
	}
}
