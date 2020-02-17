using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	[Table("users")]
	public sealed class User
	{
		/// <summary>
		///     The <see cref="Spectacles.NET.Types.User" />'s ID this entry belongs to.
		/// </summary>
		[Column("id")]
		public ulong Id { get; set; }

		[Column("command_uses")]
		public uint CommandUses { get; set; } = 0;

		[Column("banner_list")]
		public string[] BannerList { get; set; } = new string[0];

		[Column("badge_set")]
		public string[] BadgeSet { get; set; } = new string[0];

		[Column("badge_list")]
		public string[] BadgeList { get; set; } = new string[0];

		[Column("color")]
		public uint Color { get; set; } = 0x000000;

		[Column("marry")]
		public ulong[] Marry { get; set; } = new ulong[0];

		[Column("money")]
		public ulong Money { get; set; } = 0;

		[Column("points")]
		public uint Points { get; set; } = 0;

		[Column("reputations")]
		public uint Reputations { get; set; } = 0;

		[Column("theme_level")]
		public string ThemeLevel { get; set; } = "1001";

		[Column("theme_profile")]
		public string ThemeProfile { get; set; } = "0001";

		[Column("dark_theme")]
		public bool DarkTheme { get; set; } = false;

		[Column("moderation_dm")]
		public bool ModerationDm { get; set; } = true;

		[Column("next_daily")]
		public DateTime? NextDaily { get; set; } = null;

		[Column("next_reputation")]
		public DateTime? NextReputation { get; set; } = null;

		[Column("vault")]
		public ulong Vault { get; set; } = 0;

		/// <summary>
		///     The navigation property to the <see cref="Member" /> entity.
		/// </summary>
		public ICollection<Member> Member { get; set; } = null!;
	}
}
