using System.Threading.Tasks;
using Skyra.Cache.Models;
using Spectacles.NET.Types;

namespace Skyra.Utils
{
	public static class MessageUtils
	{
		public static async Task<Message> SendAsync(this Message message, Client client, string content)
		{
			return await message.SendAsync(client, new SendableMessage
			{
				Content = content
			});
		}

		public static async Task<Message> SendAsync(this Message message, Client client, SendableMessage data)
		{
			// Retrieve the previous message
			var previous = await client.Cache.EditableMessages.GetAsync(message.Id, message.ChannelId);

			// If a previous message exists...
			if (previous != null)
			{
				// Then we check whether or not it's editable (has no attachments), and we're not sending attachments
				if (message.Attachments.Count == 0 && data.File == null)
					// We update the message and return.
					return await client.Rest.Channels[message.ChannelId].Messages[previous.OwnMessageId]
						.PatchAsync<Message>(data);

				// Otherwise we delete the previous message and do a fallback.
				await client.Rest.Channels[message.ChannelId].Messages[previous.OwnMessageId].DeleteAsync();
			}

			// Send a message to Discord, receive a Message back.
			var response = await client.Rest.Channels[message.ChannelId].Messages.PostAsync<Message>(data);

			// Store the message into Redis for later processing.
			await client.Cache.EditableMessages.SetAsync(
				new CachedEditableMessage(message.Id, response.Id), message.ChannelId);

			// Return the response.
			return response;
		}

		public static async Task<Message> EditAsync(this Message message, Client client, string content)
		{
			return await message.EditAsync(client, new SendableMessage
			{
				Content = content
			});
		}

		public static async Task<Message> EditAsync(this Message message, Client client, SendableMessage data)
		{
			return await client.Rest.Channels[message.ChannelId].Messages[message.Id].PatchAsync<Message>(data);
		}
	}
}
