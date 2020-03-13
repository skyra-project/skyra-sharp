using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreGuildChannel : CoreChannel, ICoreBaseStructure<CoreGuildChannel>
	{
		public CoreGuildChannel(ulong id, ChannelType type, CoreGuild? guild, ulong guildId, string name,
			int? rawPosition, ulong parentId, CorePermissionOverwrite[] permissionOverwrites) : base(id, type)
		{
			Guild = guild;
			GuildId = guildId;
			Name = name;
			RawPosition = rawPosition;
			ParentId = parentId;
			PermissionOverwrites = permissionOverwrites;
		}

		[JsonIgnore]
		public CoreGuild? Guild { get; private set; }

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
				Guild,
				GuildId,
				Name,
				RawPosition,
				ParentId,
				PermissionOverwrites);
		}

		public new static CoreGuildChannel From(Channel channel)
		{
			return new CoreGuildChannel(ulong.Parse(channel.Id), channel.Type, null, ulong.Parse(channel.GuildId),
				channel.Name, channel.Position, ulong.Parse(channel.ParentId),
				channel.PermissionOverwrites.Select(CorePermissionOverwrite.From).ToArray());
		}

		public async Task<CoreGuild?> GetGuildAsync(Client client)
		{
			return Guild ??= await client.Cache.Guilds.GetAsync(GuildId.ToString());
		}
	}
}
