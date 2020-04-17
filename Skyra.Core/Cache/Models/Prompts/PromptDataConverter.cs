using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skyra.Core.Cache.Models.Prompts
{
	internal sealed class PromptDataConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		[NotNull]
		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
			[NotNull] JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var type = Enum.Parse<PromptDataType>((string) jo["type"]!);
			var data = jo["s"]!.CreateReader();
			IPromptData state = type switch
			{
				PromptDataType.MessageSingleUser => serializer.Deserialize<PromptDataMessage>(data)!,
				PromptDataType.ReactionSingleUser => serializer.Deserialize<PromptDataReaction>(data)!,
				PromptDataType.RichDisplay => serializer.Deserialize<RichDisplay>(data)!,
				_ => throw new ArgumentOutOfRangeException(nameof(type))
			};

			return new PromptData(null!, type, state);
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(PromptData).IsAssignableFrom(objectType);
		}
	}
}
