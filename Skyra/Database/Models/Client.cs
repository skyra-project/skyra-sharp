namespace Skyra.Database.Models
{
	public sealed class Client
	{
		public ulong Id { get; set; }
		public uint CommandUses { get; set; }
		public ulong[] UserBlacklist { get; set; }
		public ulong[] GuildBlacklist { get; set; }
		public ulong[] BoostsGuilds { get; set; }
		public ulong[] BoostsUsers { get; set; }
	}
}
