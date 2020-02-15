using System;
using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	public class SocialCounter : Monitor
	{
		public SocialCounter(Client client) : base(client,
			new MonitorOptions(nameof(SocialCounter), ignoreOthers: false, ignoreEdits: false))
		{
		}

		public override Task<bool> Run(Message message)
		{
			Console.WriteLine($"Received Message [{message.Id}] from {message.Author.Username} with content '{message.Content}'.");
			return Task.FromResult(true);
		}
	}
}
