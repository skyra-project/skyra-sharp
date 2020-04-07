using JetBrains.Annotations;
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
		public IClient Client { get; set; }

		[NotNull]
		public CoreGuildEmoji Patch([NotNull] CoreGuildEmoji value)
		{
			Animated = value.Animated;
			Name = value.Name;
			return this;
		}

		[NotNull]
		public CoreGuildEmoji Clone()
		{
			return new CoreGuildEmoji(Client,
				Id,
				Name,
				Animated);
		}

		[NotNull]
		public static CoreGuildEmoji From(IClient client, [NotNull] Emoji emoji)
		{
			return new CoreGuildEmoji(client, ulong.Parse(emoji.Id), emoji.Name, emoji.Animated ?? false);
		}
	}
}
