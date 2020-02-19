using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct RoleSet
	{
		public RoleSet(string name, ulong[] roles)
		{
			Name = name;
			Roles = roles;
		}

		/// <summary>
		///     The name for this role set.
		/// </summary>
		[JsonProperty("n")]
		public string Name { get; set; }

		/// <summary>
		///     The collection of <see cref="Spectacles.NET.Types.Role" /> that conform with this group.
		/// </summary>
		[JsonProperty("r")]
		public ulong[] Roles { get; set; }
	}
}
