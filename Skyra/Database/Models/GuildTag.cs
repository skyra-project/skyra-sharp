using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildTag
	{
		public string Name { get; set; }
		public string Content { get; set; }
	}
}