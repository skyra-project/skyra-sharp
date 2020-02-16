namespace Skyra.Database.Models
{
	public struct GuildActionAlias
	{
		public string Command { get; set; }
		public string Alias { get; set; }
	}

	public enum GuildActionTriggerTypes
	{
		React
	}

	public struct GuildActionTrigger
	{
		public GuildActionTriggerTypes Type { get; set; }
		public string Input { get; set; }
		public string Output { get; set; }
	}

	public class GuildAction
	{
		public GuildActionAlias Alias { get; set; }
		public GuildActionTrigger Trigger { get; set; }

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}
}
