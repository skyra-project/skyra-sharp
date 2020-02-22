using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class EventMessageCreate
	{
		private readonly Client _client;

		public EventMessageCreate(Client client)
		{
			_client = client;
			_client.EventHandler.OnMessageCreate += Run;
		}

		private void Run(Message message)
		{
			RunMonitors(message);
			Task.Run(() => _client.Cache.Messages.SetAsync(message));
		}

		private void RunMonitors(Message message)
		{
			foreach (var monitor in _client.Monitors.Values.Where(monitor => ShouldRunMonitor(message, monitor)))
				monitor.Method.Invoke(monitor.Instance, new object?[] {message});
		}

		private static bool ShouldRunMonitor(Message message, MonitorInfo monitor)
		{
			return monitor.AllowedTypes.Contains(message.Type)
			       && !(monitor.IgnoreBots && message.Author.Bot)
			       // && !(monitor.IgnoreSelf && message.Author.Id == Client.User.Id)
			       // && !(monitor.IgnoreOthers && message.Author.Id != Client.User.Id)
			       && !(monitor.IgnoreWebhooks && message.WebhookId != null)
			       && !(monitor.IgnoreEdits && message.EditedTimestamp != null);
		}
	}
}
