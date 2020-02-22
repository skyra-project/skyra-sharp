using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(int), "integer")]
	public class Int32Resolver
	{
		private readonly Client _client;

		public Int32Resolver(Client client)
		{
			_client = client;
		}

		public Task<int> ResolveAsync(Message message, string content)
		{
			var resolved = int.Parse(content);
			return Task.FromResult(resolved);
		}
	}
}
