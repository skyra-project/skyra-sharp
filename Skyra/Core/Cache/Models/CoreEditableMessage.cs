using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreEditableMessage : ICoreBaseStructure<CoreEditableMessage>
	{
		public CoreEditableMessage(ulong id, ulong ownMessageId)
		{
			Id = id;
			OwnMessageId = ownMessageId;
		}

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("o")]
		public ulong OwnMessageId { get; private set; }

		public CoreEditableMessage Patch(CoreEditableMessage value)
		{
			OwnMessageId = value.OwnMessageId;
			return this;
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
