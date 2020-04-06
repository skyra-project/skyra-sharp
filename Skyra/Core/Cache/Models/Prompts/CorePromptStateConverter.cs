using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Skyra.Core.Cache.Models.Prompts
{
	internal sealed class CorePromptStateConverter : JsonConverter
	{
		private Dictionary<CorePromptStateType, Func<JToken, ICorePromptState>> TypeResolvers { get; } =
			new Dictionary<CorePromptStateType, Func<JToken, ICorePromptState>>(new[]
			{
				new KeyValuePair<CorePromptStateType, Func<JToken, ICorePromptState>>(
					CorePromptStateType.MessageSingleUser,
					ParseMessageSingleUser),
				new KeyValuePair<CorePromptStateType, Func<JToken, ICorePromptState>>(
					CorePromptStateType.ReactionSingleUser,
					ParseReactionSingleUser)
			});

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
			JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var type = Enum.Parse<CorePromptStateType>((string) jo["type"]!);
			var state = TypeResolvers[type](jo["s"]!);
			return new CorePromptState(null!, type, state);
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(CorePromptState).IsAssignableFrom(objectType);
		}

		private static ICorePromptState ParseMessageSingleUser(JToken state)
		{
			return new CorePromptStateMessage((ulong) state["aid"]!, (ulong) state["cid"]!,
				state["ctx"]!);
		}

		private static ICorePromptState ParseReactionSingleUser(JToken state)
		{
			return new CorePromptStateReaction((ulong) state["aid"]!, (ulong) state["mid"]!,
				state["ctx"]!);
		}
	}
}
