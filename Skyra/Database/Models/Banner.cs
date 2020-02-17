using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("banners")]
	public sealed class Banner
	{
		/// <summary>
		///     The ID of the banner, they are 6 characters long and represent a code.
		/// </summary>
		[Column("id")]
		[MaxLength(6)]
		public string Id { get; set; } = null!;

		/// <summary>
		///     The group this banner belongs to.
		/// </summary>
		[Column("group")]
		[MaxLength(32)]
		public string Group { get; set; } = null!;

		/// <summary>
		///     The title of this banner.
		/// </summary>
		[Column("title")]
		[MaxLength(128)]
		public string Title { get; set; } = null!;

		/// <summary>
		///     The author of this banner, referencing to a Discord User ID.
		/// </summary>
		[Column("author_id")]
		public ulong AuthorId { get; set; }

		/// <summary>
		///     The price in Shiny to buy this banner.
		/// </summary>
		[Column("price")]
		public int Price { get; set; }
	}
}
