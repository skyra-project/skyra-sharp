using Newtonsoft.Json;

namespace Skyra.Cache.Models
{
	public class CachedEditableMessage
	{
		public CachedEditableMessage(string id, string ownMessageId, bool editable)
		{
			Id = id;
			OwnMessageId = ownMessageId;
			Editable = editable;
		}

		[JsonProperty("id")]
		public string Id { get; }

		[JsonProperty("o")]
		public string OwnMessageId { get; }

		[JsonProperty("e")]
		public bool Editable { get; }
	}
}
