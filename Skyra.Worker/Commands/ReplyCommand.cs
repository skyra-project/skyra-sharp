using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;

namespace Skyra.Worker.Commands
{
	[Command(Delimiter = " ", FlagSupport = true)]
	public class ReplyCommand : StructureBase
	{
		public ReplyCommand(IClient client) : base(client)
		{
		}

		public async Task RunAsync([NotNull] CoreMessage message, [Argument(Minimum = -5)] int number)
		{
			await message.SendAsync($"A number! {number.ToString()}");
		}

		public async Task RunAsync([NotNull] CoreMessage message, DateTime time,
			string content = "Something, you did not specify what")
		{
			await message.SendAsync(
				$"Alright! Reminder added. I'll remember you \"{content}\" at {time.ToShortDateString()}");
		}

		public async Task RunAsync([NotNull] CoreMessage message, string content, [NotNull] int[] integers)
		{
			await message.SendAsync(
				$"With the content of {content}, you have given me {integers.Length} number(s), with a sum of {integers.Sum()}");
		}

		public async Task RunAsync([NotNull] CoreMessage message,
			[Argument(Rest = true)] string content = "I got absolutely nothing!")
		{
			await message.SendAsync($"A string! {content}");
		}
	}
}
