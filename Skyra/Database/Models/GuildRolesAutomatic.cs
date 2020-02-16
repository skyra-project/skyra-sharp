using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildRolesAutomatic
	{
		public ulong RoleId { get; set; }
		public ulong Points { get; set; }
	}
}