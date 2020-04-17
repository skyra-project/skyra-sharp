using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class Channel : IBaseStructure<Channel>
	{
		public Channel(IClient client, ulong id, ChannelType type)
		{
			Client = client;
			Id = id;
			Type = type;
		}

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("t")]
		public ChannelType Type { get; private set; }

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public Channel Patch([NotNull] Channel value)
		{
			Type = value.Type;
			return this;
		}

		[NotNull]
		public Channel Clone()
		{
			return new Channel(Client, Id, Type);
		}

		[NotNull]
		public static Channel From(IClient client, [NotNull] Spectacles.NET.Types.Channel channel)
		{
			return new Channel(client, ulong.Parse(channel.Id), channel.Type);
		}
	}
}
