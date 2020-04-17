using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skyra.Core.Cache.Models.Prompts
{
	internal sealed class CorePromptStateConverter : JsonConverter
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
			var type = Enum.Parse<CorePromptStateType>((string) jo["type"]!);
			var data = jo["s"]!.CreateReader();
			ICorePromptState state = type switch
			{
				CorePromptStateType.MessageSingleUser => serializer.Deserialize<CorePromptStateMessage>(data)!,
				CorePromptStateType.ReactionSingleUser => serializer.Deserialize<CorePromptStateReaction>(data)!,
				CorePromptStateType.RichDisplay => serializer.Deserialize<CoreRichDisplay>(data)!,
				_ => throw new ArgumentOutOfRangeException()
			};

			return new CorePromptState(null!, type, state);
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(CorePromptState).IsAssignableFrom(objectType);
		}

	}
}
