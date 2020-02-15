using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class Moderation
    {
        public int CaseId { get; set; }
        public long? CreatedAt { get; set; }
        public int? Duration { get; set; }
        public string ExtraData { get; set; }
        public string GuildId { get; set; }
        public string ModeratorId { get; set; }
        public string Reason { get; set; }
        public string UserId { get; set; }
        public short Type { get; set; }
    }
}
