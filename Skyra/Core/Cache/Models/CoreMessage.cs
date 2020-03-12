using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreMessage : ICoreBaseStructure<CoreMessage>
	{
		public CoreMessage(ulong id, MessageType type, CoreChannel? channel, ulong channelId, CoreGuild? guild,
			ulong? guildId, CoreGuildMember? member, Webhook? webhook, ulong? webhookId, CoreUser? author,
			ulong authorId, string content, Embed[] embeds,
			Attachment[] attachments, DateTime timestamp, DateTime? editedTimestamp)
		{
			Id = id;
			Type = type;
			Channel = channel;
			ChannelId = channelId;
			Guild = guild;
			GuildId = guildId;
			Author = author;
			AuthorId = authorId;
			Member = member;
			Webhook = webhook;
			WebhookId = webhookId;
			Content = content;
			Embeds = embeds;
			Attachments = attachments;
			Timestamp = timestamp;
			EditedTimestamp = editedTimestamp;
		}

		[JsonProperty("id")]
		public ulong Id { get; private set; }

		[JsonProperty("tid")]
		[JsonConverter(typeof(StringEnumConverter))]
		public MessageType Type { get; private set; }

		[JsonIgnore]
		public CoreChannel? Channel { get; private set; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; private set; }

		[JsonIgnore]
		public CoreGuild? Guild { get; private set; }

		[JsonProperty("gid")]
		public ulong? GuildId { get; private set; }

		[JsonIgnore]
		public CoreGuildMember? Member { get; private set; }

		[JsonIgnore]
		public Webhook? Webhook { get; }

		[JsonProperty("wid")]
		public ulong? WebhookId { get; private set; }

		[JsonIgnore]
		public CoreUser? Author { get; private set; }

		[JsonProperty("aid")]
		public ulong AuthorId { get; private set; }

		[JsonProperty("c")]
		public string Content { get; private set; }

		[JsonProperty("e")]
		public Embed[] Embeds { get; private set; }

		[JsonProperty("a")]
		public Attachment[] Attachments { get; private set; }

		[JsonProperty("t")]
		public DateTime Timestamp { get; private set; }

		[JsonProperty("et")]
		public DateTime? EditedTimestamp { get; private set; }

		public CoreMessage Patch(CoreMessage value)
		{
			Content = value.Content;
			Embeds = value.Embeds;
			EditedTimestamp = value.EditedTimestamp;
			return this;
		}

		public CoreMessage Clone()
		{
			return new CoreMessage(Id,
				Type,
				Channel,
				ChannelId,
				Guild,
				GuildId,
				Member,
				Webhook,
				WebhookId,
				Author,
				AuthorId,
				Content,
				Embeds,
				Attachments,
				Timestamp,
				EditedTimestamp);
		}

		public static CoreMessage From(Message message)
		{
			return new CoreMessage(ulong.Parse(message.Id), message.Type, null, ulong.Parse(message.ChannelId), null,
				message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : CoreGuildMember.From(message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				CoreUser.From(message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(),
				message.Attachments.ToArray(), message.Timestamp, message.EditedTimestamp);
		}

		public static CoreMessage From(MessageUpdatePayload message)
		{
			return new CoreMessage(ulong.Parse(message.Id), MessageType.DEFAULT, null, ulong.Parse(message.ChannelId),
				null, message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : CoreGuildMember.From(message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				CoreUser.From(message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(),
				message.Attachments.ToArray(), message.Timestamp ?? DateTime.MinValue, message.EditedTimestamp);
		}

		public CoreMessage Patch(MessageUpdatePayload value)
		{
			Content = value.Content;
			Embeds = value.Embeds.ToArray();
			EditedTimestamp = value.EditedTimestamp;
			return this;
		}

		public async Task<CoreUser?> GetAuthorAsync(Client client)
		{
			return Author ??= await client.Cache.Users.GetAsync(AuthorId.ToString());
		}

		public async Task<CoreGuildMember?> GetMemberAsync(Client client)
		{
			return Member ??= await client.Cache.GuildMembers.GetAsync(AuthorId.ToString(), GuildId.ToString());
		}

		public async Task<CoreChannel?> GetChannelAsync(Client client)
		{
			return Channel ??= await client.Cache.GuildChannels.GetAsync(AuthorId.ToString());
		}

		public async Task<CoreGuild?> GetGuildAsync(Client client)
		{
			return Guild ??= await client.Cache.Guilds.GetAsync(AuthorId.ToString());
		}

		public async Task<CoreMessage> SendAsync(Client client, string content)
		{
			return await SendAsync(client, new SendableMessage
			{
				Content = content
			});
		}

		public async Task<CoreMessage> SendAsync(Client client, SendableMessage data)
		{
			// Cache the string values
			var id = Id.ToString();
			var channel = ChannelId.ToString();

			// Retrieve the previous message
			var previous = await client.Cache.EditableMessages.GetAsync(id, channel);

			// If a previous message exists...
			if (previous != null)
			{
				// Then we check whether or not it's editable (has no attachments), and we're not sending attachments
				if (Attachments.Length == 0 && data.File == null)
					// We update the message and return.
				{
					return From(await client.Rest.Channels[channel]
						.Messages[previous.OwnMessageId.ToString()]
						.PatchAsync<Message>(data));
				}

				// Otherwise we delete the previous message and do a fallback.
				await client.Rest.Channels[channel].Messages[previous.OwnMessageId.ToString()].DeleteAsync();
			}

			// Send a message to Discord, receive a Message back.
			var response = await client.Rest.Channels[channel].Messages.PostAsync<Message>(data);

			// Store the message into Redis for later processing.
			await client.Cache.EditableMessages.SetAsync(
				new CoreEditableMessage(Id, ulong.Parse(response.Id)), channel);

			// Return the response.
			return From(response);
		}

		public async Task<CoreMessage> EditAsync(Client client, string content)
		{
			return await EditAsync(client, new SendableMessage
			{
				Content = content
			});
		}

		public async Task<CoreMessage> EditAsync(Client client, SendableMessage data)
		{
			return From(await client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()]
				.PatchAsync<Message>(data));
		}

		public async Task<CoreMessage> DeleteAsync(Client client, string? reason)
		{
			await client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()].DeleteAsync(reason);
			return this;
		}

		public async Task CacheAsync(Client client)
		{
			var channelTask = GuildId == null
				? client.Cache.Channels.SetNullableAsync(Channel)
				: client.Cache.GuildChannels.SetNullableAsync(Channel as CoreGuildChannel, GuildId.ToString());
			var authorTask = client.Cache.Users.SetNullableAsync(Author);
			var memberTask = client.Cache.GuildMembers.SetNullableAsync(Member, GuildId.ToString());
			var messageTask = client.Cache.Messages.SetAsync(this, ChannelId.ToString());
			await Task.WhenAll(channelTask, authorTask, memberTask, messageTask);
		}
	}
}
