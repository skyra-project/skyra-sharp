using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct LevelRole
	{
		public LevelRole(ulong roleId, ulong points)
		{
			RoleId = roleId;
			Points = points;
		}

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> this entry gives.
		/// </summary>
		[JsonProperty("r")]
		public ulong RoleId { get; set; }

		/// <summary>
		///     The amount of points required for this level role.
		/// </summary>
		[JsonProperty("p")]
		public ulong Points { get; set; }
	}
}
