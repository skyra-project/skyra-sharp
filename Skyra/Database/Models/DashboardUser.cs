using System;

namespace Skyra.Database.Models
{
	public class DashboardUser
	{
		public ulong Id { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime ExpiresAt { get; set; }
	}
}
