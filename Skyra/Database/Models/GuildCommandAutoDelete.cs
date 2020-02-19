using System;
using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct GuildCommandAutoDelete
	{
		public GuildCommandAutoDelete(string command, TimeSpan duration)
		{
			Command = command;
			Duration = duration;
		}

		/// <summary>
		///     The command to be automatically deleted when running.
		/// </summary>
		[JsonProperty("c")]
		public string Command { get; set; }

		/// <summary>
		///     How long the command should stay until it's automatically deleted.
		/// </summary>
		[JsonProperty("d")]
		public TimeSpan Duration { get; set; }
	}
}
