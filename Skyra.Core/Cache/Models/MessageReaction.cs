using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class MessageReaction : IBaseStructure<MessageReaction>
	{
		public MessageReaction(IClient client, ulong messageId, ulong channelId, ulong guildId, ulong userId,
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

		public MessageReaction Patch(MessageReaction value)
		{
			throw new NotImplementedException();
		}

		[NotNull]
		public MessageReaction Clone()
		{
			return new MessageReaction(Client, MessageId, ChannelId, GuildId, UserId, Emoji);
		}

		[NotNull]
		public MessageReaction From(IClient client, [NotNull] MessageReactionAddPayload reaction)
		{
			return new MessageReaction(client, ulong.Parse(reaction.MessageId), ulong.Parse(reaction.ChannelId),
				ulong.Parse(reaction.GuildId), ulong.Parse(reaction.UserId), reaction.Emoji);
		}
	}
}
