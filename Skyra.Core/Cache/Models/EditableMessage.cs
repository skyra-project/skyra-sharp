using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class EditableMessage : IBaseStructure<EditableMessage>
	{
		public EditableMessage(IClient client, ulong id, ulong ownMessageId)
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
		public EditableMessage Patch([NotNull] EditableMessage value)
		{
			OwnMessageId = value.OwnMessageId;
			return this;
		}

		[NotNull]
		public EditableMessage Clone()
		{
			return new EditableMessage(Client, Id, OwnMessageId);
		}

		[ItemCanBeNull]
		public async Task<Message?> GetMessageAsync()
		{
			return await Client.Cache.Messages.GetAsync(OwnMessageId.ToString());
		}
	}
}
