using System;
using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	public class SocialCounter : Monitor
	{
		public SocialCounter(Client client) : base(client,
			new MonitorOptions {Name = "SocialCounter", IgnoreOthers = false})
		{
		}

		public override Task<bool> Run(Message message)
		{
			Console.WriteLine($"Received Message [{message.Id}] from {message.Author.Username}.");
			return Task.FromResult(true);
		}
	}
}
