using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreChannel : ICoreBaseStructure<CoreChannel>
	{
		public CoreChannel(IClient client, ulong id, ChannelType type)
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
		public IClient Client { get; }

		public CoreChannel Patch(CoreChannel value)
		{
			Type = value.Type;
			return this;
		}

		public CoreChannel Clone()
		{
			return new CoreChannel(Client, Id, Type);
		}

		public static CoreChannel From(IClient client, Channel channel)
		{
			return new CoreChannel(client, ulong.Parse(channel.Id), channel.Type);
		}
	}
}
