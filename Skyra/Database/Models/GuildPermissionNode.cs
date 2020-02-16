using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildPermissionNode
	{
		public GuildPermissionNode(ulong id, string[] commands)
		{
			Id = id;
			Commands = commands;
		}

		public ulong Id { get; set; }
		public string[] Commands { get; set; }
	}
}
