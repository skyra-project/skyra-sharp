using System.Linq;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildChannel : CoreChannel, ICoreBaseStructure<CoreGuildChannel>
	{
		public CoreGuildChannel(Channel channel) : base(channel)
		{
			GuildId = ulong.Parse(channel.GuildId);
			Name = channel.Name;
			RawPosition = channel.Position;
			ParentId = ulong.Parse(channel.ParentId);
			PermissionOverwrites = channel.PermissionOverwrites.Select(o => new CorePermissionOverwrite(o)).ToArray();
		}

		[JsonConstructor]
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

		public void Patch(CoreGuildChannel value)
		{
			base.Patch(value);
			Name = value.Name;
			RawPosition = value.RawPosition;
			ParentId = value.ParentId;
			PermissionOverwrites = value.PermissionOverwrites;
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
	}
}
