using System.Threading.Tasks;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(int))]
	public class Int32Resolver
	{
		public Task<int> ResolveAsync(Message message, string content)
		{
			var resolved = int.Parse(content);
			return Task.FromResult(resolved);
		}
	}
}
