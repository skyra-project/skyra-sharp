namespace Skyra.Database.Models
{
	public sealed class GuildPermission
	{
		public ulong Id { get; set; }
		public GuildPermissionNode[] Users { get; set; } = new GuildPermissionNode[0];
		public GuildPermissionNode[] Roles { get; set; } = new GuildPermissionNode[0];

		public ulong GuildId { get; set; }
		public Guild Guild { get; set; }
	}
}