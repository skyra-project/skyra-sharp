using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	public class GuildAction
	{
		public GuildActionAlias Alias { get; set; }
		public GuildActionTrigger Trigger { get; set; }

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	[ComplexType]
	public class GuildActionAlias
	{
		public string Command { get; set; }
		public string Alias { get; set; }
	}

	public enum GuildActionTriggerTypes
	{
		React
	}

	[ComplexType]
	public class GuildActionTrigger
	{
		public GuildActionTriggerTypes Type { get; set; }
		public string Input { get; set; }
		public string Output { get; set; }
	}
}
