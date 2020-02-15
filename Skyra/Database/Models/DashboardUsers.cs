using System;
using System.Collections.Generic;

namespace Skyra.Database.Models
{
    public class DashboardUsers
    {
        public string Id { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresAt { get; set; }
    }
}
