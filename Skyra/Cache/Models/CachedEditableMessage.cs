using Newtonsoft.Json;

namespace Skyra.Cache.Models
{
	public class CachedEditableMessage
	{
		public CachedEditableMessage(string id, string ownMessageId)
		{
			Id = id;
			OwnMessageId = ownMessageId;
		}

		[JsonProperty("id")]
		public string Id { get; }

		[JsonProperty("o")]
		public string OwnMessageId { get; }
	}
}
