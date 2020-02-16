namespace Skyra.Database.Models
{
	public class GuildSocial
	{
		public bool Enabled { get; set; } = true;
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public ulong? LevelUpChannel { get; set; } = null;
		public float Multiplier { get; set; } = 1.0f;

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}
}
