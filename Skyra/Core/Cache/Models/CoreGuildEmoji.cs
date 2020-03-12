using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildEmoji : ICoreBaseStructure<CoreGuildEmoji>
	{
		public CoreGuildEmoji(ulong id, string name, bool animated)
		{
			Id = id;
			Name = name;
			Animated = animated;
		}

		[JsonProperty("id")]
		public ulong Id { get; set; }

		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonProperty("a")]
		public bool Animated { get; set; }

		public CoreGuildEmoji Patch(CoreGuildEmoji value)
		{
			Animated = value.Animated;
			Name = value.Name;
			return this;
		}

		public CoreGuildEmoji Clone()
		{
			return new CoreGuildEmoji(Id,
				Name,
				Animated);
		}

		public static CoreGuildEmoji From(Emoji emoji)
		{
			return new CoreGuildEmoji(ulong.Parse(emoji.Id), emoji.Name, emoji.Animated ?? false);
		}
	}
}
