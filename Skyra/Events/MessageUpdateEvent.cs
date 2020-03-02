using System;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class MessageUpdateEvent : StructureBase
	{
		public MessageUpdateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnMessageUpdate += Run;
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
				var previousMessage = await Client.Cache.Messages.GetAsync(messageUpdate.Id);
				var message = GenerateMessage(messageUpdate, previousMessage);

				await Client.Cache.Messages.SetAsync(new CoreMessage(message));
				RunMonitors(message);
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine($"Error! {exception.Message}: ${exception.StackTrace}");
			}
		}

		private static Message GenerateMessage(MessageUpdatePayload messageUpdate, CoreMessage? previousMessage)
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
			foreach (var monitor in Client.Monitors.Values)
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
