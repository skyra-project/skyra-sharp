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
			Task.Run(() => RunMonitorsAsync(message));
		}

		private async Task RunMonitorsAsync(MessageUpdatePayload messageUpdate)
		{
			var previousMessage = await Client.Cache.Messages.GetAsync(messageUpdate.Id);
			var message = previousMessage == null
				? new CoreMessage(messageUpdate)
				: previousMessage.Patch(messageUpdate);

			await message.CacheAsync(Client);
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
