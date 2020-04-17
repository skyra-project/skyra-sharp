using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public struct PermissionOverwrite
	{
		public static PermissionOverwrite From([NotNull] Spectacles.NET.Types.PermissionOverwrite overwrite)
		{
			return new PermissionOverwrite(ulong.Parse(overwrite.Id),
				overwrite.Type == "role" ? PermissionOverwriteType.Role : PermissionOverwriteType.Member,
				overwrite.Allow, overwrite.Deny);
		}

		public PermissionOverwrite(ulong id, PermissionOverwriteType type, Permission allow, Permission deny)
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
		public PermissionOverwriteType Type { get; set; }

		/// <summary>permission bit set</summary>
		[JsonProperty("allow")]
		public Permission Allow { get; set; }

		/// <summary>permission bit set</summary>
		[JsonProperty("deny")]
		public Permission Deny { get; set; }
	}
}
