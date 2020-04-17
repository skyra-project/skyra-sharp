using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Skyra.Core.Cache.Models.Prompts
{
	public sealed class PromptData : IBaseStructure<PromptData>
	{
		public PromptData(IClient client, PromptDataType type, IPromptData state)
		{
			Client = client;
			Type = type;
			Data = state;
		}

		[JsonProperty("s")]
		public IPromptData Data { get; private set; }

		[JsonProperty("type")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PromptDataType Type { get; }

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public PromptData Patch([NotNull] PromptData value)
		{
			return this;
		}

		[NotNull]
		public PromptData Clone()
		{
			return new PromptData(Client, Type, Data);
		}
	}
}
