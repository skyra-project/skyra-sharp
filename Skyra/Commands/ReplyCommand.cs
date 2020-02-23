using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command(Delimiter = " ")]
	public class ReplyCommand
	{
		private readonly Client _client;

		public ReplyCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message, int number)
		{
			await message.SendAsync(_client, $"A number! {number.ToString()}");
		}

		public async Task RunAsync(Message message, DateTime time, string content = default)
		{
			var m = string.IsNullOrEmpty(content) ? "Something, you did not specify what" : content;
			await message.SendAsync(_client, $"Alright! Reminder added. I'll remember you \"{m}\" at {time.ToShortDateString()}");
		}

		public async Task RunAsync(Message message, string content = default)
		{
			var m = string.IsNullOrEmpty(content) ? "I got absolutely nothing!" : content;
			await message.SendAsync(_client, $"A string! {m}");
		}
	}
}
