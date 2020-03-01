using System;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command(Delimiter = " ", FlagSupport = true)]
	public class ReplyCommand : StructureBase
	{
		public ReplyCommand(Client client) : base(client)
		{
		}

		public async Task RunAsync(Message message, [Argument(Minimum = -5)] int number)
		{
			await message.SendAsync(Client, $"A number! {number.ToString()}");
		}

		public async Task RunAsync(Message message, DateTime time,
			string content = "Something, you did not specify what")
		{
			await message.SendAsync(Client,
				$"Alright! Reminder added. I'll remember you \"{content}\" at {time.ToShortDateString()}");
		}

		public async Task RunAsync(Message message, string content, int[] integers)
		{
			await message.SendAsync(Client,
				$"With the content of {content}, you have given me {integers.Length} number(s), with a sum of {integers.Sum()}");
		}

		public async Task RunAsync(Message message,
			[Argument(Rest = true)] string content = "I got absolutely nothing!")
		{
			await message.SendAsync(Client, $"A string! {content}");
		}
	}
}
