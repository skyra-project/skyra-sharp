using System.Collections.Generic;
using System.Threading.Tasks;
using Spectacles.NET.Types;
using StackExchange.Redis;

namespace Skyra.Cache.Stores
{
	public class MessageStore : CacheStore<Message>
	{
		public MessageStore(CacheClient client) : base(client, "messages")
		{
		}

		public override async Task SetAsync(Message entry, string? parent = null)
		{
			var temporaryMessage = new Message
			{
				Id = entry.Id,
				Content = entry.Content,
				Embeds = entry.Embeds,
				Type = entry.Type,
				Timestamp = entry.Timestamp,
				ChannelId = entry.ChannelId,
				EditedTimestamp = entry.EditedTimestamp,
				GuildId = entry.GuildId,
				WebhookId = entry.WebhookId
			};

			await Task.WhenAll(
				Client.Users.SetAsync(entry.Author),
				Client.Members.SetAsync(entry.Member),
				Database.HashSetAsync(FormatKeyName(parent),
					new[] {new HashEntry(entry.Id, SerializeValue(temporaryMessage))}));
		}

		public override Task SetAsync(IEnumerable<Message> entries, string? parent = null)
		{
			throw new System.NotImplementedException();
		}
	}
}
