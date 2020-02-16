using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public class GuildCommandAutoDelete
	{
		public string Command { get; set; }
		public TimeSpan Duration { get; set; }
	}
}