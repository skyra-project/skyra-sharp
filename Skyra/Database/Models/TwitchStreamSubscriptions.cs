using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class TwitchStreamSubscriptions
    {
        public string Id { get; set; }
        public bool IsStreaming { get; set; }
        public long ExpiresAt { get; set; }
        public string[] GuildIds { get; set; }
    }
}
