using System;
using System.Threading.Tasks;
using Skyra.Cache.Models;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	public class EventMessageEdit : Event
	{
		public EventMessageEdit(Client client) : base(client, new EventOptions(nameof(EventMessageCreate)))
		{
			EventHandler.OnMessageUpdate += Run;
		}

		private void Run(MessageUpdatePayload message)
			=> Task.Run(() => RunAsync(message));

		private async Task RunAsync(MessageUpdatePayload messageUpdate)
		{
			// TODO: Use this for logging and stuff when more core stuff is done
			var previousMessage = await Client.Cache.Messages.GetAsync(messageUpdate.Id);
			var message = GenerateMessage(messageUpdate, previousMessage);

			await Task.WhenAll(Client.Cache.Messages.SetAsync(message),
				Client.Monitors.Run(message));
		}

		private static Message GenerateMessage(MessageUpdatePayload messageUpdate, CachedMessage? previousMessage)
			=> new Message
			{
				Id = messageUpdate.Id,
				Content = messageUpdate.Content,
				Embeds = messageUpdate.Embeds,
				Type = messageUpdate.Type ?? MessageType.DEFAULT,
				Timestamp = messageUpdate.Timestamp ?? previousMessage?.Timestamp ?? DateTime.MinValue,
				ChannelId = messageUpdate.ChannelId,
				EditedTimestamp = messageUpdate.EditedTimestamp,
				GuildId = messageUpdate.GuildId,
				WebhookId = messageUpdate.WebhookId
			};
	}
}
