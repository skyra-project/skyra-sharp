using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildStickyRole
	{
		public ulong User { get; set; }
		public ulong[] Roles { get; set; }
	}
}