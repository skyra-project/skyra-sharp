using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Events
{
	[Event]
	public class MessageCreateEvent : StructureBase
	{
		public MessageCreateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnMessageCreate += Run;
		}

		private void Run(CoreMessage message)
		{
			Task.Run(() => RunAsync(message));
		}

		private async Task RunAsync(CoreMessage message)
		{
			foreach (var monitor in Client.Monitors.Values.Where(m => ShouldRunMonitor(message, m)))
			{
				try
				{
					await (Task) monitor.Method.Invoke(monitor.Instance, new object?[] {message})!;
				}
				catch (TargetInvocationException exception)
				{
					await Console.Error.WriteLineAsync($"[MONITORS]: {monitor.Name}");
					await Console.Error.WriteLineAsync($"ERROR: {exception.InnerException?.Message ?? exception.Message}");
					await Console.Error.WriteLineAsync($"ERROR: {exception.InnerException?.StackTrace ?? exception.StackTrace}");
				}
				catch (Exception exception)
				{
					await Console.Error.WriteLineAsync($"[MONITORS]: {monitor.Name}");
					await Console.Error.WriteLineAsync($"ERROR: {exception.Message}");
					await Console.Error.WriteLineAsync($"ERROR: {exception.StackTrace}");
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
