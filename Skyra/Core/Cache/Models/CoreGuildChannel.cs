using System.Linq;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildChannel : CoreChannel, ICoreBaseStructure<CoreGuildChannel>
	{
		public CoreGuildChannel(ulong id, ChannelType type, ulong guildId, string name, int? rawPosition,
			ulong parentId, CorePermissionOverwrite[] permissionOverwrites) : base(id, type)
		{
			GuildId = guildId;
			Name = name;
			RawPosition = rawPosition;
			ParentId = parentId;
			PermissionOverwrites = permissionOverwrites;
		}

		[JsonProperty("gid")]
		public ulong GuildId { get; set; }

		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonProperty("rp")]
		public int? RawPosition { get; set; }

		[JsonProperty("pid")]
		public ulong ParentId { get; set; }

		[JsonProperty("po")]
		public CorePermissionOverwrite[] PermissionOverwrites { get; set; }

		public CoreGuildChannel Patch(CoreGuildChannel value)
		{
			base.Patch(value);
			Name = value.Name;
			RawPosition = value.RawPosition;
			ParentId = value.ParentId;
			PermissionOverwrites = value.PermissionOverwrites;
			return this;
		}

		public new CoreGuildChannel Clone()
		{
			return new CoreGuildChannel(Id,
				Type,
				GuildId,
				Name,
				RawPosition,
				ParentId,
				PermissionOverwrites);
		}

		public new static CoreGuildChannel From(Channel channel)
		{
			return new CoreGuildChannel(ulong.Parse(channel.Id), channel.Type, ulong.Parse(channel.GuildId),
				channel.Name, channel.Position, ulong.Parse(channel.ParentId),
				channel.PermissionOverwrites.Select(CorePermissionOverwrite.From).ToArray());
		}
	}
}
