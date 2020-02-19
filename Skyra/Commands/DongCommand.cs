using System;
using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command]
	public class DongCommand : ICommand
	{
		private Client _client;
		public string Delimiter => "";

		public DongCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message)
		{
			await _client.Rest.Channels[message.ChannelId].Messages.PostAsync<SendableMessage>(new SendableMessage
			{
				Content = "Dong to you"
			});
		}
	}
}
