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
			var previous = await client.Cache.EditableMessages.GetAsync(message.Id, message.ChannelId);
			if (previous != null)
				return await client.Rest.Channels[message.ChannelId].Messages[previous.OwnMessageId]
					.PatchAsync<Message>(data);

			var response = await client.Rest.Channels[message.ChannelId].Messages.PostAsync<Message>(data);
			await client.Cache.EditableMessages.SetAsync(
				new CachedEditableMessage(message.Id, response.Id, response.Attachments.Count == 0), message.ChannelId);
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
