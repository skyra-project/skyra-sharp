using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventMessageEdit : Event
	{
		public EventMessageEdit(Client client) : base(client, new EventOptions(nameof(EventMessageEdit)))
		{
			EventHandler.OnMessageUpdate += Run;
		}

		private void Run(MessageUpdatePayload message)
		{
			Task.Run(() => RunAsync(message));
		}

		private async Task RunAsync(MessageUpdatePayload messageUpdate)
		{
			try
			{
				var previousMessage = await Client.Cache.Messages.GetAsync(messageUpdate.Id);
				var message = GenerateMessage(messageUpdate, previousMessage);

				await Task.WhenAll(Client.Cache.Messages.SetAsync(new CachedMessage(message)),
					Client.Monitors.Run(message));
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"Error! {exception.Message}: ${exception.StackTrace}");
			}
		}

		private static Message GenerateMessage(MessageUpdatePayload messageUpdate, CachedMessage? previousMessage)
		{
			return new Message
			{
				Id = messageUpdate.Id,
				Author = messageUpdate.Author,
				Member = messageUpdate.Member,
				Content = messageUpdate.Content,
				Embeds = messageUpdate.Embeds,
				Attachments = messageUpdate.Attachments,
				Type = messageUpdate.Type ?? MessageType.DEFAULT,
				Timestamp = messageUpdate.Timestamp ?? previousMessage?.Timestamp ?? DateTime.MinValue,
				ChannelId = messageUpdate.ChannelId,
				GuildId = messageUpdate.GuildId,
				EditedTimestamp = messageUpdate.EditedTimestamp,
				WebhookId = messageUpdate.WebhookId
			};
		}
	}
}
