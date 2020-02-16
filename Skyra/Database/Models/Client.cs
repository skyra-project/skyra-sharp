namespace Skyra.Database.Models
{
	public class Client
	{
		public ulong Id { get; set; }
		public int CommandUses { get; set; } = 0;
		public ulong[] UserBlacklist { get; set; } = new ulong[0];
		public ulong[] GuildBlacklist { get; set; } = new ulong[0];
		public ulong[] BoostsGuilds { get; set; } = new ulong[0];
		public ulong[] BoostsUsers { get; set; } = new ulong[0];
	}
}
