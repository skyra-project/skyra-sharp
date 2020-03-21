using System;
using System.Linq;
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
		public MessageCreateEvent(IClient client) : base(client)
		{
			Client.EventHandler.OnMessageCreateAsync += RunAsync;
		}

		private async Task RunAsync(CoreMessage message)
		{
			foreach (var monitor in Client.Monitors.Values.Where(m => ShouldRunMonitor(message, m)))
			{
				try
				{
					await monitor.Instance.RunAsync(message);
				}
				catch (Exception exception)
				{
					Client.Logger.Error("[MONITORS]: {Name} | {Exception}", monitor.Name, exception);
				}
			}
		}

		private bool ShouldRunMonitor(CoreMessage message, MonitorInfo monitor)
		{
			return monitor.AllowedTypes.Contains(message.Type)
			       && !(monitor.IgnoreBots && message.Author!.Bot)
			       && !(monitor.IgnoreSelf && message.Author!.Id == Client.Id)
			       && !(monitor.IgnoreOthers && message.Author!.Id != Client.Id)
			       && !(monitor.IgnoreWebhooks && message.WebhookId != null)
			       && !(monitor.IgnoreEdits && message.EditedTimestamp != null);
		}
	}
}
