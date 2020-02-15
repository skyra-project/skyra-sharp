using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class Starboard
    {
        public bool Enabled { get; set; }
        public string UserId { get; set; }
        public string MessageId { get; set; }
        public string ChannelId { get; set; }
        public string GuildId { get; set; }
        public string StarMessageId { get; set; }
        public int Stars { get; set; }
    }
}
