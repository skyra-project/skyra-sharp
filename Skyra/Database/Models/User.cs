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

		/// <summary>
		///     The amount of commands this user has run.
		/// </summary>
		[Column("command_uses")]
		public uint CommandUses { get; set; } = 0;

		/// <summary>
		///     The banners this user has in their inventory.
		/// </summary>
		[Column("banner_list")]
		public string[] BannerList { get; set; } = new string[0];

		/// <summary>
		///     The badges this user has set in their profile.
		/// </summary>
		[Column("badge_set")]
		public string[] BadgeSet { get; set; } = new string[0];

		/// <summary>
		///     The badges this user has in their inventory.
		/// </summary>
		[Column("badge_list")]
		public string[] BadgeList { get; set; } = new string[0];

		/// <summary>
		///     The user's preferred color.
		/// </summary>
		[Column("color")]
		public uint Color { get; set; } = 0x000000;

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.User" /> IDs this user is married to.
		/// </summary>
		[Column("married_to")]
		public ulong[] MarriedTo { get; set; } = new ulong[0];

		/// <summary>
		///     The amount of money this user has.
		/// </summary>
		[Column("money")]
		public ulong Money { get; set; } = 0;

		/// <summary>
		///     The amount of money this user has protected in the vault.
		/// </summary>
		[Column("vault")]
		public ulong Vault { get; set; } = 0;

		/// <summary>
		///     The amount of points this user has.
		/// </summary>
		[Column("points")]
		public uint Points { get; set; } = 0;

		/// <summary>
		///     The amount of reputations this user has.
		/// </summary>
		[Column("reputations")]
		public uint Reputations { get; set; } = 0;

		/// <summary>
		///     The banner theme set for the level command.
		/// </summary>
		[Column("theme_level")]
		public string ThemeLevel { get; set; } = "1001";

		/// <summary>
		///     The banner theme set for the profile command.
		/// </summary>
		[Column("theme_profile")]
		public string ThemeProfile { get; set; } = "0001";

		/// <summary>
		///     Whether or not this user prefers dark theme.
		/// </summary>
		[Column("dark_theme")]
		public bool DarkTheme { get; set; } = false;

		/// <summary>
		///     Whether or not this user wants to receive Direct Messages from the moderation module
		/// </summary>
		[Column("moderation_dm")]
		public bool ModerationDm { get; set; } = true;

		/// <summary>
		///     The refresh time for dailies. Refreshes once every 12h and has a grace period of 1h.
		/// </summary>
		[Column("next_daily")]
		public DateTime? NextDaily { get; set; } = null;

		/// <summary>
		///     The refresh time for reputations. Refreshes once every 24h.
		/// </summary>
		[Column("next_reputation")]
		public DateTime? NextReputation { get; set; } = null;

		/// <summary>
		///     The navigation property to the <see cref="Member" /> entity.
		/// </summary>
		public ICollection<Member> Member { get; set; } = null!;
	}
}
