using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreMessageReaction : ICoreBaseStructure<CoreMessageReaction>
	{
		public CoreMessageReaction(IClient client, ulong messageId, ulong channelId, ulong guildId, ulong userId,
			Emoji emoji)
		{
			Client = client;
			MessageId = messageId;
			ChannelId = channelId;
			GuildId = guildId;
			UserId = userId;
			Emoji = emoji;
		}

		[JsonProperty("mid")]
		public ulong MessageId { get; private set; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; private set; }

		[JsonProperty("gid")]
		public ulong GuildId { get; private set; }

		[JsonProperty("uid")]
		public ulong UserId { get; private set; }

		[JsonProperty("e")]
		public Emoji Emoji { get; private set; }

		[JsonIgnore]
		public IClient Client { get; set; }

		public CoreMessageReaction Patch(CoreMessageReaction value)
		{
			throw new NotImplementedException();
		}

		[NotNull]
		public CoreMessageReaction Clone()
		{
			return new CoreMessageReaction(Client, MessageId, ChannelId, GuildId, UserId, Emoji);
		}

		[NotNull]
		public CoreMessageReaction From(IClient client, [NotNull] MessageReactionAddPayload reaction)
		{
			return new CoreMessageReaction(client, ulong.Parse(reaction.MessageId), ulong.Parse(reaction.ChannelId),
				ulong.Parse(reaction.GuildId), ulong.Parse(reaction.UserId), reaction.Emoji);
		}
	}
}
