using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class GuildChannel : Channel, IBaseStructure<GuildChannel>
	{
		public GuildChannel(IClient client, ulong id, ChannelType type, Guild? guild, ulong guildId,
			string name, int? rawPosition, ulong parentId, PermissionOverwrite[] permissionOverwrites) : base(
			client, id, type)
		{
			Guild = guild;
			GuildId = guildId;
			Name = name;
			RawPosition = rawPosition;
			ParentId = parentId;
			PermissionOverwrites = permissionOverwrites;
		}

		[JsonIgnore]
		public Guild? Guild { get; private set; }

		[JsonProperty("gid")]
		public ulong GuildId { get; set; }

		[JsonProperty("n")]
		public string Name { get; set; }

		[JsonProperty("rp")]
		public int? RawPosition { get; set; }

		[JsonProperty("pid")]
		public ulong ParentId { get; set; }

		[JsonProperty("po")]
		public PermissionOverwrite[] PermissionOverwrites { get; set; }

		[NotNull]
		public GuildChannel Patch([NotNull] GuildChannel value)
		{
			base.Patch(value);
			Name = value.Name;
			RawPosition = value.RawPosition;
			ParentId = value.ParentId;
			PermissionOverwrites = value.PermissionOverwrites;
			return this;
		}

		[NotNull]
		public new GuildChannel Clone()
		{
			return new GuildChannel(Client,
				Id,
				Type,
				Guild,
				GuildId,
				Name,
				RawPosition,
				ParentId,
				PermissionOverwrites);
		}

		[ItemCanBeNull]
		public async Task<Guild?> GetGuildAsync(IClient client)
		{
			return Guild ??= await client.Cache.Guilds.GetAsync(GuildId.ToString());
		}

		[NotNull]
		public new static GuildChannel From(IClient client, [NotNull] Spectacles.NET.Types.Channel channel)
		{
			return new GuildChannel(client, ulong.Parse(channel.Id), channel.Type, null,
				ulong.Parse(channel.GuildId),
				channel.Name, channel.Position, ulong.Parse(channel.ParentId),
				channel.PermissionOverwrites.Select(PermissionOverwrite.From).ToArray());
		}
	}
}
