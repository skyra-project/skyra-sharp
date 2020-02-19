using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct GuildPermissionNode
	{
		[JsonProperty("i")]
		public ulong Id { get; set; }

		/// <summary>
		///     The commands to be allowed for this permission node
		/// </summary>
		[JsonProperty("a")]
		public string[] Allowed { get; set; }

		/// <summary>
		///     The commands to be disallowed for this permission node
		/// </summary>
		[JsonProperty("d")]
		public string[] Disallowed { get; set; }
	}
}
