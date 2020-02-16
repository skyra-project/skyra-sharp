namespace Skyra.Database.Models
{
	public sealed class GuildSocial
	{
		public ulong Id { get; set; }
		public bool Enabled { get; set; } = true;
		public ulong[] IgnoredChannels { get; set; } = new ulong[0];
		public ulong? LevelUpChannel { get; set; } = null;
		public float Multiplier { get; set; } = 1.0f;

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}
