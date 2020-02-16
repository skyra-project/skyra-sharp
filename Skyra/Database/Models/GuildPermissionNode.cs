using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildPermissionNode
	{
		public ulong Id { get; set; }
		public string[] Commands { get; set; }
	}
}