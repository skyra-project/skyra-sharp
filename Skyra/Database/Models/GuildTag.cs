using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[ComplexType]
	public sealed class GuildTag
	{
		public GuildTag(string name, string content)
		{
			Name = name;
			Content = content;
		}

		public string Name { get; set; }
		public string Content { get; set; }
	}
}
