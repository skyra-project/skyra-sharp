using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
	public sealed class User
	{
		public ulong Id { get; set; }
		public uint CommandUses { get; set; } = 0;
		public string[] BannerList { get; set; } = new string[0];
		public string[] BadgeSet { get; set; } = new string[0];
		public string[] BadgeList { get; set; } = new string[0];
		public uint Color { get; set; } = 0x000000;
		public ulong[] Marry { get; set; } = new ulong[0];
		public ulong Money { get; set; } = 0;
		public uint PointCount { get; set; } = 0;
		public uint ReputationCount { get; set; } = 0;
		public string ThemeLevel { get; set; } = "1001";
		public string ThemeProfile { get; set; } = "0001";
		public bool DarkTheme { get; set; } = false;
		public bool ModerationDm { get; set; } = true;
		public DateTime? NextDaily { get; set; } = null;
		public DateTime? NextReputation { get; set; } = null;
		public ulong Vault { get; set; } = 0;

		public ICollection<Member> Member { get; set; }
	}
}
