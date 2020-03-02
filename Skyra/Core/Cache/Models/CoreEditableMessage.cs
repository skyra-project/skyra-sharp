using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public class CoreEditableMessage : ICoreBaseStructure<CoreEditableMessage>
	{
		public CoreEditableMessage(string id, string ownMessageId)
		{
			Id = id;
			OwnMessageId = ulong.Parse(ownMessageId);
		}

		[JsonConstructor]
		public CoreEditableMessage(string id, ulong ownMessageId)
		{
			Id = id;
			OwnMessageId = ownMessageId;
		}

		[JsonProperty("id")]
		public string Id { get; private set; }

		[JsonProperty("o")]
		public ulong OwnMessageId { get; private set; }

		public void Patch(CoreEditableMessage value)
		{
			OwnMessageId = value.OwnMessageId;
		}

		public CoreEditableMessage Clone()
		{
			return new CoreEditableMessage(Id, OwnMessageId);
		}

		public async Task<CoreMessage?> GetMessageAsync(Client client)
		{
			return await client.Cache.Messages.GetAsync(OwnMessageId.ToString());
		}
	}
}
