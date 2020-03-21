using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreGuildEmoji : ICoreBaseStructure<CoreGuildEmoji>
	{
		public CoreGuildEmoji(IClient client, ulong id, string name, bool animated)
		{
			Client = client;
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

		[JsonIgnore]
		public IClient Client { get; }

		public CoreGuildEmoji Patch(CoreGuildEmoji value)
		{
			Animated = value.Animated;
			Name = value.Name;
			return this;
		}

		public CoreGuildEmoji Clone()
		{
			return new CoreGuildEmoji(Client,
				Id,
				Name,
				Animated);
		}

		public static CoreGuildEmoji From(IClient client, Emoji emoji)
		{
			return new CoreGuildEmoji(client, ulong.Parse(emoji.Id), emoji.Name, emoji.Animated ?? false);
		}
	}
}
