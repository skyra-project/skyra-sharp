using System.Threading.Tasks;
using Skyra.Utils;
using Spectacles.NET.Types;

namespace Skyra.Commands
{
	[Command(Delimiter = " ")]
	public class ReplyCommandCommand
	{
		private Client _client;

		public ReplyCommandCommand(Client client) => _client = client;

		public async Task RunAsync(Message message, int number)
		{
			await message.SendAsync(_client, number.ToString());
		}

	}
}
