using Newtonsoft.Json;

namespace Skyra.Core.Database.Models
{
	public class GuildDisabledCommandChannels
	{
		public GuildDisabledCommandChannels(ulong channelId, string[] commands)
		{
			ChannelId = channelId;
			Commands = commands;
		}

		/// <summary>
		///     The <see cref="Spectacles.NET.Types.Channel" />
		/// </summary>
		[JsonProperty("c")]
		public ulong ChannelId { get; set; }

		/// <summary>
		///     The commands that are disabled for this entry's <see cref="ChannelId" />
		/// </summary>
		[JsonProperty("c")]
		public string[] Commands { get; set; }
	}
}
