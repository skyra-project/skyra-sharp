using Newtonsoft.Json;

namespace Skyra.Database.Models
{
	public struct GuildTag
	{
		public GuildTag(string name, string content)
		{
			Name = name;
			Content = content;
		}

		/// <summary>
		///     The tag's name.
		/// </summary>
		[JsonProperty("n")]
		public string Name { get; set; }

		/// <summary>
		///     The tag's content
		/// </summary>
		[JsonProperty("c")]
		public string Content { get; set; }
	}
}
