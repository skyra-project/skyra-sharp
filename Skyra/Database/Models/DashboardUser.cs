using System;

namespace Skyra.Database.Models
{
	public sealed class DashboardUser
	{
		public ulong Id { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime ExpiresAt { get; set; }
	}
}
