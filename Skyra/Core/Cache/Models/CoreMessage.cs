using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Skyra.Core.Database;
using Skyra.Resources;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class CoreMessage : ICoreBaseStructure<CoreMessage>
	{
		public CoreMessage(IClient client, ulong id, MessageType type, CoreChannel? channel, ulong channelId,
			CoreGuild? guild, ulong? guildId, CoreGuildMember? member, Webhook? webhook, ulong? webhookId,
			CoreUser? author, ulong authorId, string content, Embed[] embeds, Attachment[] attachments,
			DateTime timestamp, DateTime? editedTimestamp, CultureInfo? language)
		{
			Client = client;
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

		[JsonIgnore]
		public IClient Client { get; set; }

		[NotNull]
		public CoreMessage Patch([NotNull] CoreMessage value)
		{
			Content = value.Content;
			Embeds = value.Embeds;
			EditedTimestamp = value.EditedTimestamp;
			return this;
		}

		[NotNull]
		public CoreMessage Clone()
		{
			return new CoreMessage(Client,
				Id,
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

		[NotNull]
		public CoreMessage Patch([NotNull] MessageUpdatePayload value)
		{
			Content = value.Content;
			Embeds = value.Embeds.ToArray();
			EditedTimestamp = value.EditedTimestamp;
			return this;
		}

		[ItemCanBeNull]
		public async Task<CoreUser?> GetAuthorAsync()
		{
			return Author ??= await Client.Cache.Users.GetAsync(AuthorId.ToString());
		}

		[ItemCanBeNull]
		public async Task<CoreGuildMember?> GetMemberAsync()
		{
			return Member ??= await Client.Cache.GuildMembers.GetAsync(AuthorId.ToString(), GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<CoreChannel?> GetChannelAsync()
		{
			return Channel ??= GuildId == null
				? await Client.Cache.Channels.GetAsync(ChannelId.ToString())
				: await Client.Cache.GuildChannels.GetAsync(ChannelId.ToString(), GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<CoreGuild?> GetGuildAsync()
		{
			if (GuildId == null) return null;
			return Guild ??= await Client.Cache.Guilds.GetAsync(GuildId.ToString()!);
		}

		public async Task<CultureInfo> GetLanguageAsync()
		{
			if (!(Language is null)) return Language;

			await using var db = new SkyraDatabaseContext();
			var guild = await db.Guilds.FindAsync(GuildId);
			var languageId = guild is null ? "en-US" : guild.Language;
			return Language = Client.Cultures[languageId];
		}

		[ItemNotNull]
		public async Task<CoreMessage> SendAsync(string content)
		{
			return await SendAsync(new SendableMessage
			{
				Content = content
			});
		}

		[ItemNotNull]
		public async Task<CoreMessage> SendLocaleAsync([Localizable(true)] string key)
		{
			var language = await GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await SendAsync(content);
		}

		[ItemNotNull]
		[StringFormatMethod("key")]
		public async Task<CoreMessage> SendLocaleAsync([Localizable(true)] string key,
			[NotNull] params object?[] values)
		{
			var language = await GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await SendAsync(string.Format(content, values));
		}

		[ItemNotNull]
		public async Task<CoreMessage> SendAsync(SendableMessage data)
		{
			// Cache the string values
			var id = Id.ToString();
			var channel = ChannelId.ToString();

			// Retrieve the previous message
			var previous = await Client.Cache.EditableMessages.GetAsync(id, channel);

			CoreMessage response;

			// If a previous message exists...
			if (previous != null)
			{
				// Then we check whether or not it's editable (has no attachments), and we're not sending attachments
				if (Attachments.Length == 0 && data.File == null)
				{
					// We update the message and return.
					response = From(Client, await Client.Rest.Channels[channel]
						.Messages[previous.OwnMessageId.ToString()]
						.PatchAsync<Message>(data));
					response.GuildId = GuildId;
					return response;
				}

				// Otherwise we delete the previous message and do a fallback.
				await Client.Rest.Channels[channel].Messages[previous.OwnMessageId.ToString()].DeleteAsync();
			}

			// Send a message to Discord, receive a Message back.
			response = From(Client, await Client.Rest.Channels[channel].Messages.PostAsync<Message>(data));
			response.GuildId = GuildId;

			// Store the message into Redis for later processing.
			await Client.Cache.EditableMessages.SetAsync(
				new CoreEditableMessage(Client, Id, response.Id), channel);

			// Return the response.
			return response;
		}

		[ItemNotNull]
		public async Task<CoreMessage> EditAsync(string content)
		{
			return await EditAsync(new SendableMessage
			{
				Content = content
			});
		}

		[ItemNotNull]
		public async Task<CoreMessage> EditAsync(SendableMessage data)
		{
			return From(Client, await Client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()]
				.PatchAsync<Message>(data));
		}

		[ItemNotNull]
		[StringFormatMethod("key")]
		public async Task<CoreMessage> EditLocaleAsync(string key)
		{
			var language = await GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await EditAsync(content);
		}

		[ItemNotNull]
		[StringFormatMethod("key")]
		public async Task<CoreMessage> EditLocaleAsync(string key, [NotNull] params object?[] values)
		{
			var language = await GetLanguageAsync();
			var content = Languages.ResourceManager.GetString(key, language) ??
			              throw new Exception($"Cannot find key {key}");
			return await EditAsync(string.Format(content, values));
		}

		[ItemNotNull]
		public async Task<CoreMessage> DeleteAsync(string? reason)
		{
			await Client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()].DeleteAsync(reason);
			return this;
		}

		public async Task CacheAsync()
		{
			var channelTask = GuildId == null
				? Client.Cache.Channels.SetNullableAsync(Channel)
				: Client.Cache.GuildChannels.SetNullableAsync(Channel as CoreGuildChannel, GuildId.ToString());
			var authorTask = Client.Cache.Users.SetNullableAsync(Author);
			var memberTask = Client.Cache.GuildMembers.SetNullableAsync(Member, GuildId.ToString());
			var messageTask = Client.Cache.Messages.SetAsync(this, ChannelId.ToString());
			await Task.WhenAll(channelTask, authorTask, memberTask, messageTask);
		}

		[NotNull]
		public static CoreMessage From(IClient client, [NotNull] Message message)
		{
			return new CoreMessage(client, ulong.Parse(message.Id), message.Type, null, ulong.Parse(message.ChannelId),
				null,
				message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : CoreGuildMember.From(client, message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				CoreUser.From(client, message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(), message.Attachments.ToArray(), message.Timestamp, message.EditedTimestamp,
				null);
		}

		[NotNull]
		public static CoreMessage From(IClient client, [NotNull] MessageUpdatePayload message)
		{
			return new CoreMessage(client, ulong.Parse(message.Id), MessageType.DEFAULT, null,
				ulong.Parse(message.ChannelId),
				null, message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : CoreGuildMember.From(client, message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				CoreUser.From(client, message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(), message.Attachments.ToArray(), message.Timestamp ?? DateTime.MinValue,
				message.EditedTimestamp, null);
		}
	}
}
