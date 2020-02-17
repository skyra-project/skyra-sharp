using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct GuildAction
	{
		public GuildAction(GuildActionTypes type, string input, string output)
		{
			Type = type;
			Input = input;
			Output = output;
		}

		/// <summary>
		///     The type this action refers to.
		/// </summary>
		[JsonProperty("t")]
		public GuildActionTypes Type { get; set; }

		/// <summary>
		///     The input which triggers this action.
		/// </summary>
		[JsonProperty("i")]
		public string Input { get; set; }

		/// <summary>
		///     The output for this action.
		/// </summary>
		[JsonProperty("o")]
		public string Output { get; set; }
	}
}
