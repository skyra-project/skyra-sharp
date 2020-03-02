using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public struct CorePermissionOverwrite
	{
		public CorePermissionOverwrite(PermissionOverwrite overwrite)
		{
			Id = ulong.Parse(overwrite.Id);
			Type = overwrite.Type == "role" ? CorePermissionOverwriteType.Role : CorePermissionOverwriteType.Member;
			Allow = overwrite.Allow;
			Deny = overwrite.Deny;
		}

		[JsonConstructor]
		public CorePermissionOverwrite(ulong id, CorePermissionOverwriteType type, Permission allow, Permission deny)
		{
			Id = id;
			Type = type;
			Allow = allow;
			Deny = deny;
		}

		/// <summary>role or user id</summary>
		[JsonProperty("id")]
		public ulong Id { get; set; }

		/// <summary>either "role" or "member"</summary>
		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public CorePermissionOverwriteType Type { get; set; }

		/// <summary>permission bit set</summary>
		[JsonProperty("allow")]
		public Permission Allow { get; set; }

		/// <summary>permission bit set</summary>
		[JsonProperty("deny")]
		public Permission Deny { get; set; }
	}
}