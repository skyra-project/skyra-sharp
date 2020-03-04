using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Events
{
	[Event]
	public class MessageCreateEvent : StructureBase
	{
		public MessageCreateEvent(Client client) : base(client)
		{
			Client.EventHandler.OnMessageCreate += Run;
		}

		private void Run(Message message)
		{
			Task.Run(() => RunMonitors(new CoreMessage(message)));
		}

		private async Task RunMonitors(CoreMessage message)
		{
			await message.CacheAsync(Client);
			foreach (var monitor in Client.Monitors.Values.Where(monitor => ShouldRunMonitor(message, monitor)))
			{
				monitor.Method.Invoke(monitor.Instance, new object?[] {message});
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
