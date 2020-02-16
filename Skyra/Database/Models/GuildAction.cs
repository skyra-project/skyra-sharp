using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildAction
	{
		public GuildAction(GuildActionTypes type, string input, string output)
		{
			Type = type;
			Input = input;
			Output = output;
		}

		public GuildActionTypes Type { get; set; }
		public string Input { get; set; }
		public string Output { get; set; }
	}
}
