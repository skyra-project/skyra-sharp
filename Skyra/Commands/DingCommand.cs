using System;
using System.Threading.Tasks;
using Skyra.Structures;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command]
	public class DingCommand : ICommand
	{
		private Client _client;

		public string Delimiter => "";

		public DingCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message)
		{
			//TODO(KyraNET): find what the hell is the problem here and why it's throwing
			Console.WriteLine("IM RUNNING");
			try
			{
				await _client.Rest.Guilds[message.GuildId].Channels[message.ChannelId].PostAsync(new Message
				{
					Content = "ding your ass hoe"
				});
			}
			catch (Exception e)
			{
				Console.WriteLine("!!!!!");
				Console.WriteLine(e.ToString());
				Console.WriteLine("!!!!!");
			}


		}
	}
}
