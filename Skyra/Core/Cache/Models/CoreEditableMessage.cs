using System.Threading.Tasks;
using JetBrains.Annotations;
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

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("o")]
		public ulong OwnMessageId { get; private set; }

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public CoreEditableMessage Patch([NotNull] CoreEditableMessage value)
		{
			OwnMessageId = value.OwnMessageId;
			return this;
		}

		[NotNull]
		public CoreEditableMessage Clone()
		{
			return new CoreEditableMessage(Client, Id, OwnMessageId);
		}

		[ItemCanBeNull]
		public async Task<CoreMessage?> GetMessageAsync()
		{
			return await Client.Cache.Messages.GetAsync(OwnMessageId.ToString());
		}
	}
}
