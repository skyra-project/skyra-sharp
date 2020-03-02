using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildEmoji : ICoreBaseStructure<CoreGuildEmoji>
	{
		public CoreGuildEmoji(Emoji emoji)
		{
			Id = ulong.Parse(emoji.Id);
			Animated = emoji.Animated ?? false;
			Name = emoji.Name;
		}

		[JsonConstructor]
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

		public void Patch(CoreGuildEmoji value)
		{
			Animated = value.Animated;
			Name = value.Name;
		}

		public CoreGuildEmoji Clone()
		{
			return new CoreGuildEmoji(Id,
				Name,
				Animated);
		}
	}
}
