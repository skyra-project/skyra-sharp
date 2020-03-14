using System;
using System.Globalization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Skyra.Core.Database;
using Skyra.Resources;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreMessage : ICoreBaseStructure<CoreMessage>
	{
		public CoreMessage(ulong id, MessageType type, CoreChannel? channel, ulong channelId, CoreGuild? guild,
			ulong? guildId, CoreGuildMember? member, Webhook? webhook, ulong? webhookId, CoreUser? author,
			ulong authorId, string content, Embed[] embeds, Attachment[] attachments, DateTime timestamp,
			DateTime? editedTimestamp, CultureInfo? language)
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
			Language = language;
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

		[JsonIgnore]
		public CultureInfo? Language { get; private set; }

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
				EditedTimestamp,
				Language);
		}

		public static CoreMessage From(Message message)
		{
			return new CoreMessage(ulong.Parse(message.Id), message.Type, null, ulong.Parse(message.ChannelId), null,
				message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : CoreGuildMember.From(message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				CoreUser.From(message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(), message.Attachments.ToArray(), message.Timestamp, message.EditedTimestamp,
				null);
		}

		public static CoreMessage From(MessageUpdatePayload message)
		{
			return new CoreMessage(ulong.Parse(message.Id), MessageType.DEFAULT, null, ulong.Parse(message.ChannelId),
				null, message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : CoreGuildMember.From(message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				CoreUser.From(message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(), message.Attachments.ToArray(), message.Timestamp ?? DateTime.MinValue,
				message.EditedTimestamp, null);
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
			return Channel ??= GuildId == null
				? await client.Cache.Channels.GetAsync(ChannelId.ToString())
				: await client.Cache.GuildChannels.GetAsync(ChannelId.ToString(), GuildId.ToString());
		}

		public async Task<CoreGuild?> GetGuildAsync(Client client)
		{
			if (GuildId == null) return null;
			return Guild ??= await client.Cache.Guilds.GetAsync(GuildId.ToString()!);
		}

		public async Task<CultureInfo> GetLanguageAsync(Client client)
		{
			if (!(Language is null)) return Language;

			await using var db = new SkyraDatabaseContext();
			var guild = await db.Guilds.FindAsync(GuildId);
			var languageId = guild is null ? "en-US" : guild.Language;
			return Language = client.Cultures[languageId];
		}

		public async Task<CoreMessage> SendAsync(Client client, string content)
		{
			return await SendAsync(client, new SendableMessage
			{
				Content = content
			});
		}

		public async Task<CoreMessage> SendLocaleAsync(Client client, string key)
		{
			var language = await GetLanguageAsync(client);
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await SendAsync(client, content);
		}

		public async Task<CoreMessage> SendLocaleAsync(Client client, string key, object?[] values)
		{
			var language = await GetLanguageAsync(client);
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await SendAsync(client, string.Format(content, values));
		}

		public async Task<CoreMessage> SendAsync(Client client, SendableMessage data)
		{
			// Cache the string values
			var id = Id.ToString();
			var channel = ChannelId.ToString();

			// Retrieve the previous message
			var previous = await client.Cache.EditableMessages.GetAsync(id, channel);

			CoreMessage response;

			// If a previous message exists...
			if (previous != null)
			{
				// Then we check whether or not it's editable (has no attachments), and we're not sending attachments
				if (Attachments.Length == 0 && data.File == null)
				{
					// We update the message and return.
					response = From(await client.Rest.Channels[channel]
						.Messages[previous.OwnMessageId.ToString()]
						.PatchAsync<Message>(data));
					response.GuildId = GuildId;
					return response;
				}

				// Otherwise we delete the previous message and do a fallback.
				await client.Rest.Channels[channel].Messages[previous.OwnMessageId.ToString()].DeleteAsync();
			}

			// Send a message to Discord, receive a Message back.
			response = From(await client.Rest.Channels[channel].Messages.PostAsync<Message>(data));
			response.GuildId = GuildId;

			// Store the message into Redis for later processing.
			await client.Cache.EditableMessages.SetAsync(
				new CoreEditableMessage(Id, response.Id), channel);

			// Return the response.
			return response;
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

		public async Task<CoreMessage> EditLocaleAsync(Client client, string key)
		{
			var language = await GetLanguageAsync(client);
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await EditAsync(client, content);
		}

		public async Task<CoreMessage> EditLocaleAsync(Client client, string key, object?[] values)
		{
			var language = await GetLanguageAsync(client);
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await EditAsync(client, string.Format(content, values));
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
