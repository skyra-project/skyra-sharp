using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildDisabledCommandChannels
	{
		public ulong Channel { get; set; }
		public string[] Commands { get; set; }
	}
}