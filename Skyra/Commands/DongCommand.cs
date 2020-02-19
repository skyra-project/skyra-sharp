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
			await _client.Rest.Guilds[message.GuildId].Channels[message.ChannelId].PostAsync(new Message
			{
				Content = "Dong your ass hoe"
			});
		}
	}
}
