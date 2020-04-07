using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class CorePromptState : ICoreBaseStructure<CorePromptState>
	{
		public CorePromptState(IClient client, CorePromptStateType type, ICorePromptState state)
		{
			Client = client;
			Type = type;
			State = state;
		}

		[JsonProperty("s")]
		public ICorePromptState State { get; private set; }

		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public CorePromptStateType Type { get; }

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public CorePromptState Patch([NotNull] CorePromptState value)
		{
			return Patch(value.State);
		}

		[NotNull]
		public CorePromptState Clone()
		{
			return new CorePromptState(Client, Type, State);
		}

		[NotNull]
		public CorePromptState Patch(ICorePromptState value)
		{
			State = State.Patch(value);
			return this;
		}
	}
}
