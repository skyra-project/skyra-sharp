using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class ClientStorage
    {
        public string Id { get; set; }
        public int CommandUses { get; set; }
        public string[] UserBlacklist { get; set; }
        public string[] GuildBlacklist { get; set; }
        public string[] Schedules { get; set; }
        public string[] BoostsGuilds { get; set; }
        public string[] BoostsUsers { get; set; }
    }
}
