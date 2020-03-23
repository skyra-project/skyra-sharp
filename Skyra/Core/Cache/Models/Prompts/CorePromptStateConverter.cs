using System;
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

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			var jo = JObject.Load(reader);
			var rawType = (string) jo["type"]!;
			var rawState = jo["s"]!;

			CorePromptStateType type;
			ICorePromptState state;
			if (rawType == "MessageSingleUser")
			{
				type = CorePromptStateType.MessageSingleUser;
				state = new CorePromptStateMessage((ulong) rawState["aid"]!, (ulong) rawState["cid"]!, rawState["ctx"]!);
			}
			else
			{
				type = CorePromptStateType.ReactionSingleUser;
				state = new CorePromptStateReaction((ulong) rawState["aid"]!, (ulong) rawState["mid"]!, rawState["ctx"]!);
			}

			return new CorePromptState(null!, type, state);
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(CorePromptState).IsAssignableFrom(objectType);
		}
	}
}
