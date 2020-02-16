using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildCommandAutoDelete
	{
		public GuildCommandAutoDelete(string command, TimeSpan duration)
		{
			Command = command;
			Duration = duration;
		}

		public string Command { get; set; }
		public TimeSpan Duration { get; set; }
	}
}
