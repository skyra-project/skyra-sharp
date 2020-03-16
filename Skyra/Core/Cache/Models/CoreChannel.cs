using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreChannel : ICoreBaseStructure<CoreChannel>
	{
		public CoreChannel(ulong id, ChannelType type)
		{
			Id = id;
			Type = type;
		}

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("t")]
		public ChannelType Type { get; private set; }

		public CoreChannel Patch(CoreChannel value)
		{
			Type = value.Type;
			return this;
		}

		public CoreChannel Clone()
		{
			return new CoreChannel(Id, Type);
		}

		public static CoreChannel From(Channel channel)
		{
			return new CoreChannel(ulong.Parse(channel.Id), channel.Type);
		}
	}
}
