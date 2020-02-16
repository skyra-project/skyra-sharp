using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildActionTrigger
	{
		public GuildActionTriggerTypes Type { get; set; }
		public string Input { get; set; }
		public string Output { get; set; }
	}
}