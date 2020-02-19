using System.Threading.Tasks;
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
			return await client.Rest.Channels[message.ChannelId].Messages.PostAsync<Message>(data);
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
