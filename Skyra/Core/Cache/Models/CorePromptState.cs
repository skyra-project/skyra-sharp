using Newtonsoft.Json;

namespace Skyra.Core.Cache.Models
{
	public sealed class CorePromptState : ICoreBaseStructure<CorePromptState>
	{
		public CorePromptState(IClient client, ulong id, ICorePromptState state)
		{
			Client = client;
			Id = id;
			State = state;
		}

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("s")]
		public ICorePromptState State { get; private set; }

		[JsonIgnore]
		public IClient Client { get; set; }

		public CorePromptState Patch(CorePromptState value)
		{
			return Patch(value.State);
		}

		public CorePromptState Clone()
		{
			return new CorePromptState(Client, Id, State);
		}

		public CorePromptState Patch(ICorePromptState value)
		{
			State = State.Patch(value);
			return this;
		}
	}
}
