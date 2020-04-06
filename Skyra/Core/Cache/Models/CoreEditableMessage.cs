using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreEditableMessage : ICoreBaseStructure<CoreEditableMessage>
	{
		public CoreEditableMessage(IClient client, ulong id, ulong ownMessageId)
		{
			Client = client;
			Id = id;
			OwnMessageId = ownMessageId;
		}

		[JsonProperty("id")] public ulong Id { get; private set; }

		[JsonProperty("o")] public ulong OwnMessageId { get; private set; }

		[JsonIgnore] public IClient Client { get; set; }

		public CoreEditableMessage Patch(CoreEditableMessage value)
		{
			OwnMessageId = value.OwnMessageId;
			return this;
		}

		public CoreEditableMessage Clone()
		{
			return new CoreEditableMessage(Client, Id, OwnMessageId);
		}

		public async Task<CoreMessage?> GetMessageAsync()
		{
			return await Client.Cache.Messages.GetAsync(OwnMessageId.ToString());
		}
	}
}
