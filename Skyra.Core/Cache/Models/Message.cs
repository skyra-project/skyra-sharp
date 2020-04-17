using System;
using System.Globalization;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Skyra.Core.Database;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public sealed class Message : IBaseStructure<Message>
	{
		public Message(IClient client, ulong id, MessageType type, Channel? channel, ulong channelId,
			Guild? guild, ulong? guildId, GuildMember? member, Webhook? webhook, ulong? webhookId,
			User? author, ulong authorId, string content, Embed[] embeds, Attachment[] attachments,
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
		public Channel? Channel { get; private set; }

		[JsonProperty("cid")]
		public ulong ChannelId { get; private set; }

		[JsonIgnore]
		public Guild? Guild { get; private set; }

		[JsonProperty("gid")]
		public ulong? GuildId { get; private set; }

		[JsonIgnore]
		public GuildMember? Member { get; private set; }

		[JsonIgnore]
		public Webhook? Webhook { get; }

		[JsonProperty("wid")]
		public ulong? WebhookId { get; private set; }

		[JsonIgnore]
		public User? Author { get; private set; }

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
		public Message Patch([NotNull] Message value)
		{
			Content = value.Content;
			Embeds = value.Embeds;
			EditedTimestamp = value.EditedTimestamp;
			return this;
		}

		[NotNull]
		public Message Clone()
		{
			return new Message(Client,
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
		public Message Patch([NotNull] MessageUpdatePayload value)
		{
			Content = value.Content;
			Embeds = value.Embeds.ToArray();
			EditedTimestamp = value.EditedTimestamp;
			return this;
		}

		[ItemCanBeNull]
		public async Task<User?> GetAuthorAsync()
		{
			return Author ??= await Client.Cache.Users.GetAsync(AuthorId.ToString());
		}

		[ItemCanBeNull]
		public async Task<GuildMember?> GetMemberAsync()
		{
			return Member ??= await Client.Cache.GuildMembers.GetAsync(AuthorId.ToString(), GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<Channel?> GetChannelAsync()
		{
			return Channel ??= GuildId == null
				? await Client.Cache.Channels.GetAsync(ChannelId.ToString())
				: await Client.Cache.GuildChannels.GetAsync(ChannelId.ToString(), GuildId.ToString());
		}

		[ItemCanBeNull]
		public async Task<Guild?> GetGuildAsync()
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
		public async Task<Message> SendAsync(string content)
		{
			return await SendAsync(new SendableMessage
			{
				Content = content
			});
		}

		[ItemNotNull]
		public async Task<Message> SendAsync(SendableMessage data)
		{
			// Cache the string values
			var id = Id.ToString();
			var channel = ChannelId.ToString();

			// Retrieve the previous message
			var previous = await Client.Cache.EditableMessages.GetAsync(id, channel);

			Message response;

			// If a previous message exists...
			if (previous != null)
			{
				// Then we check whether or not it's editable (has no attachments), and we're not sending attachments
				if (Attachments.Length == 0 && data.File == null)
				{
					// We update the message and return.
					response = From(Client, await Client.Rest.Channels[channel]
						.Messages[previous.OwnMessageId.ToString()]
						.PatchAsync<Spectacles.NET.Types.Message>(data));
					response.GuildId = GuildId;
					return response;
				}

				// Otherwise we delete the previous message and do a fallback.
				await Client.Rest.Channels[channel].Messages[previous.OwnMessageId.ToString()].DeleteAsync();
			}

			// Send a message to Discord, receive a Message back.
			response = From(Client,
				await Client.Rest.Channels[channel].Messages.PostAsync<Spectacles.NET.Types.Message>(data));
			response.GuildId = GuildId;

			// Store the message into Redis for later processing.
			await Client.Cache.EditableMessages.SetAsync(
				new EditableMessage(Client, Id, response.Id), channel);

			// Return the response.
			return response;
		}

		[ItemNotNull]
		public async Task<Message> EditAsync(string content)
		{
			return await EditAsync(new SendableMessage
			{
				Content = content
			});
		}

		[ItemNotNull]
		public async Task<Message> EditAsync(SendableMessage data)
		{
			return From(Client, await Client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()]
				.PatchAsync<Spectacles.NET.Types.Message>(data));
		}

		[ItemNotNull]
		public async Task<Message> DeleteAsync(string? reason)
		{
			await Client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()].DeleteAsync(reason);
			return this;
		}

		[ItemNotNull]
		public async Task<object> ReactAsync(string reaction)
		{
			return await Client.Rest.Channels[ChannelId.ToString()].Messages[Id.ToString()].Reactions[reaction]["@me"]
				.PutAsync<object>(null);
		}

		public async Task CacheAsync()
		{
			var channelTask = GuildId == null
				? Client.Cache.Channels.SetNullableAsync(Channel)
				: Client.Cache.GuildChannels.SetNullableAsync(Channel as GuildChannel, GuildId.ToString());
			var authorTask = Client.Cache.Users.SetNullableAsync(Author);
			var memberTask = Client.Cache.GuildMembers.SetNullableAsync(Member, GuildId.ToString());
			var messageTask = Client.Cache.Messages.SetAsync(this, ChannelId.ToString());
			await Task.WhenAll(channelTask, authorTask, memberTask, messageTask);
		}

		[NotNull]
		public static Message From(IClient client, [NotNull] Spectacles.NET.Types.Message message)
		{
			return new Message(client, ulong.Parse(message.Id), message.Type, null, ulong.Parse(message.ChannelId),
				null,
				message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : GuildMember.From(client, message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				User.From(client, message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(), message.Attachments.ToArray(), message.Timestamp, message.EditedTimestamp,
				null);
		}

		[NotNull]
		public static Message From(IClient client, [NotNull] MessageUpdatePayload message)
		{
			return new Message(client, ulong.Parse(message.Id), MessageType.DEFAULT, null,
				ulong.Parse(message.ChannelId),
				null, message.GuildId == null ? (ulong?) null : ulong.Parse(message.GuildId),
				message.Member == null ? null : GuildMember.From(client, message.Member, message.Author), null,
				message.WebhookId == null ? (ulong?) null : ulong.Parse(message.WebhookId),
				User.From(client, message.Author), ulong.Parse(message.Author.Id), message.Content,
				message.Embeds.ToArray(), message.Attachments.ToArray(), message.Timestamp ?? DateTime.MinValue,
				message.EditedTimestamp, null);
		}
	}
}
