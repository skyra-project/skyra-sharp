using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class Users
    {
        public string Id { get; set; }
        public int CommandUses { get; set; }
        public string[] BannerList { get; set; }
        public string[] BadgeSet { get; set; }
        public string[] BadgeList { get; set; }
        public int Color { get; set; }
        public string[] Marry { get; set; }
        public long Money { get; set; }
        public int PointCount { get; set; }
        public int ReputationCount { get; set; }
        public string ThemeLevel { get; set; }
        public string ThemeProfile { get; set; }
        public bool DarkTheme { get; set; }
        public bool? ModerationDm { get; set; }
        public long? NextDaily { get; set; }
        public long? NextReputation { get; set; }
        public int Vault { get; set; }
    }
}
