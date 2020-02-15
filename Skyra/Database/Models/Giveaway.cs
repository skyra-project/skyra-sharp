using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class Giveaway
    {
        public string Title { get; set; }
        public long EndsAt { get; set; }
        public string GuildId { get; set; }
        public string ChannelId { get; set; }
        public string MessageId { get; set; }
        public int Minimum { get; set; }
        public int MinimumWinners { get; set; }
    }
}
