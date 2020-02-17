using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct GuildStickyRole
	{
		public GuildStickyRole(ulong userId, ulong[] roles)
		{
			UserId = userId;
			Roles = roles;
		}

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.User" />'s ID this entry refers to.
		/// </summary>
		[JsonProperty("u")]
		public ulong UserId { get; set; }

		/// <summary>
		///     The roles to be stuck with this entry's user.
		/// </summary>
		[JsonProperty("r")]
		public ulong[] Roles { get; set; }
	}
}
