using System.ComponentModel.DataAnnotations;

namespace Skyra.Database.Models
{
	public class Banner
	{
		[MaxLength(6)]
		public string Id { get; set; }

		[MaxLength(32)]
		public string Group { get; set; }

		[MaxLength(128)]
		public string Title { get; set; }

		public ulong AuthorId { get; set; }
		public int Price { get; set; }
	}
}
