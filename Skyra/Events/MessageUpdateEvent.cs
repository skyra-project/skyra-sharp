using System;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class MessageUpdateEvent
	{
		private readonly Client _client;

		public MessageUpdateEvent(Client client)
		{
			_client = client;
			_client.EventHandler.OnMessageUpdate += Run;
		}

		private void Run(MessageUpdatePayload message)
		{
			Task.Run(() => RunAsync(message));
		}

		private async Task RunAsync(MessageUpdatePayload messageUpdate)
		{
			try
			{
				// TODO(kyranet): Pull message.Author from Redis or fetch, Discord sometimes doesn't give it.
				var previousMessage = await _client.Cache.Messages.GetAsync(messageUpdate.Id);
				var message = GenerateMessage(messageUpdate, previousMessage);

				await _client.Cache.Messages.SetAsync(new CachedMessage(message));
				RunMonitors(message);
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

		private void RunMonitors(Message message)
		{
			foreach (var monitor in _client.Monitors.Values)
			{
				try
				{
					monitor.Method.Invoke(monitor.Instance, new object?[] {message});
				}
				catch (TargetInvocationException exception)
				{
					Console.Error.WriteLine($"[MONITORS]: {monitor.Name}");
					Console.Error.WriteLine($"ERROR: {exception.InnerException?.Message ?? exception.Message}");
					Console.Error.WriteLine($"ERROR: {exception.InnerException?.StackTrace ?? exception.StackTrace}");
				}
			}
		}
	}
}
