using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skyra.Database.Models
{
	// TODO(kyranet): Discuss with the team whether or not we want to migrate this to Redis.
	// TODO(kyranet): Investigate which one of PGSQL or Redis is safer for sensitive information.
	// TODO(kyranet): Find a way to securely encrypt this data.
	public sealed class DashboardUser
	{
		/// <summary>
		///     The Discord User <see cref="Spectacles.NET.Types.User.Id" /> that owns this entry.
		/// </summary>
		[Column("id")]
		public ulong Id { get; set; }

		/// <summary>
		///     The access token used by the end-user to retrieve information from Discord.
		/// </summary>
		[Column("access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		///     The refresh token used to get a new <see cref="AccessToken" /> before <see cref="ExpiresAt" /> runs out.
		/// </summary>
		[Column("refresh_token")]
		public string RefreshToken { get; set; }

		/// <summary>
		///     The time in which this entry will be automatically invalidated and removed from the database.
		/// </summary>
		[Column("expires_at")]
		public DateTime ExpiresAt { get; set; }
	}
}
