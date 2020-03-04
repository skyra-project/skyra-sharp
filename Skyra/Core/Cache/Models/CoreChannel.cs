using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreChannel : ICoreBaseStructure<CoreChannel>
	{
		public CoreChannel(Channel channel)
		{
			Id = ulong.Parse(channel.Id);
			Type = channel.Type;
		}

		[JsonConstructor]
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
	}
}
