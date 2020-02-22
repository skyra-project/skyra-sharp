using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Monitors
{
	[Monitor(IgnoreOthers = false, IgnoreEdits = false)]
	public class SocialCounter
	{
		public SocialCounter(Client _)
		{
		}

		public Task RunAsync(Message message)
		{
			Console.WriteLine(
				$"Received Message [{message.Id}] from {message.Author.Username} with content '{message.Content}'.");
			return Task.FromResult(true);
		}
	}
}
