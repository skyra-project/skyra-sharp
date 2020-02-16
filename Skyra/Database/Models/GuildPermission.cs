namespace Skyra.Database.Models
{
	public class GuildPermission
	{
		public GuildPermissionNode[] Users { get; set; } = new GuildPermissionNode[0];

		public GuildPermissionNode[] Roles { get; set; } = new GuildPermissionNode[0];

		public ulong GuildId { get; set; }
		public virtual Guild Guild { get; set; }
	}

	public struct GuildPermissionNode
	{
		public ulong Id { get; set; }
		public string[] Commands { get; set; }
	}
}
