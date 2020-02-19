using System.Threading.Tasks;
using Skyra.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command]
	public class DingCommand
	{
		private readonly Client _client;

		public DingCommand(Client client)
		{
			_client = client;
		}

		public async Task RunAsync(Message message)
		{
			await message.SendAsync(_client, "Ding to you");
		}
	}
}
