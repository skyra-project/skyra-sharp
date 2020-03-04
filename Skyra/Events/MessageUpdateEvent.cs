using System;
using System.Linq;
using System.Reflection;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class MessageUpdateEvent : StructureBase
	{
		public MessageUpdateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnMessageUpdate += Run;
		}

		private void Run(CoreMessage? _, CoreMessage message)
		{
			foreach (var monitor in Client.Monitors.Values.Where(m => ShouldRunMonitor(message, m)))
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

		private static bool ShouldRunMonitor(CoreMessage message, MonitorInfo monitor)
		{
			return monitor.AllowedTypes.Contains(message.Type)
			       && !(monitor.IgnoreBots && message.Author!.Bot)
			       // && !(monitor.IgnoreSelf && message.Author.Id == Client.User.Id)
			       // && !(monitor.IgnoreOthers && message.Author.Id != Client.User.Id)
			       && !(monitor.IgnoreWebhooks && message.WebhookId != null)
			       && !(monitor.IgnoreEdits && message.EditedTimestamp != null);
		}
	}
}
