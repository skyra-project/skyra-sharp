using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildDisabledCommandChannels
	{
		public GuildDisabledCommandChannels(ulong channel, string[] commands)
		{
			Channel = channel;
			Commands = commands;
		}

		public ulong Channel { get; set; }
		public string[] Commands { get; set; }
	}
}
