using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct ReactionRole
	{
		public ReactionRole(ulong roleId, ulong channelId, ulong messageId)
		{
			RoleId = roleId;
			ChannelId = channelId;
			MessageId = messageId;
		}

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Role" /> that is to be given.
		/// </summary>
		[JsonProperty("r")]
		public ulong RoleId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" /> this reaction role is available at.
		/// </summary>
		[JsonProperty("c")]
		public ulong ChannelId { get; set; }

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Message" /> this reaction role is available at.
		/// </summary>
		[JsonProperty("m")]
		public ulong MessageId { get; set; }
	}
}
