using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Rest.APIError;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command]
	public class DingCommand
	{
		private Client _client;

		public DingCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message)
		{
			//TODO(KyraNET): find what the hell is the problem here and why it's throwing
			Console.WriteLine("IM RUNNING");

			await _client.Rest.Channels[message.ChannelId].Messages.PostAsync<SendableMessage>(new SendableMessage
			{
				Content = "Ding to you",
			});
		}
	}
}
