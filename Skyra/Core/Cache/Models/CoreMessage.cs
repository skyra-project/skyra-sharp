using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spectacles.NET.Types;

namespace Skyra.Core.Cache.Models
{
	public class CoreMessage : ICoreBaseStructure<CoreMessage>
	{
		public CoreMessage(Message message)
		{
			Id = message.Id;
			ChannelId = message.ChannelId;
			GuildId = message.GuildId;
			AuthorId = message.Author.Id;
			Content = message.Content;
			Embeds = message.Embeds;
			Timestamp = message.Timestamp;
			Attachments = message.Attachments;
			EditedTimestamp = message.EditedTimestamp;
		}

		[JsonConstructor]
		public CoreMessage(string id, string channelId, string guildId, string authorId, string content,
			List<Embed> embeds,
			DateTime timestamp, List<Attachment> attachments, DateTime? editedTimestamp)
		{
			Id = id;
			ChannelId = channelId;
			GuildId = guildId;
			AuthorId = authorId;
			Content = content;
			Embeds = embeds;
			Timestamp = timestamp;
			Attachments = attachments;
			EditedTimestamp = editedTimestamp;
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("cid")]
		public string ChannelId { get; set; }

		[JsonProperty("gid")]
		public string GuildId { get; set; }

		[JsonProperty("aid")]
		public string AuthorId { get; set; }

		[JsonProperty("c")]
		public string Content { get; set; }

		[JsonProperty("e")]
		public List<Embed> Embeds { get; set; }

		[JsonProperty("a")]
		public List<Attachment> Attachments { get; set; }

		[JsonProperty("t")]
		public DateTime Timestamp { get; set; }

		[JsonProperty("et")]
		public DateTime? EditedTimestamp { get; set; }

		public void Patch(CoreMessage value)
		{
			Content = value.Content;
			Embeds = value.Embeds;
			EditedTimestamp = value.EditedTimestamp;
		}

		public CoreMessage Clone()
		{
			return new CoreMessage(Id,
				ChannelId,
				GuildId,
				AuthorId,
				Content,
				Embeds,
				Timestamp,
				Attachments,
				EditedTimestamp);
		}

		public async Task<CoreUser?> GetAuthorAsync(Client client)
		{
			return await client.Cache.Users.GetAsync(AuthorId);
		}

		public async Task<CoreGuildChannel?> GetChannelAsync(Client client)
		{
			return await client.Cache.GuildChannels.GetAsync(AuthorId);
		}

		public async Task<CoreGuild?> GetGuildAsync(Client client)
		{
			return await client.Cache.Guilds.GetAsync(AuthorId);
		}
	}
}
