using System;
using System.Threading.Tasks;
using Spectacles.NET.Types;

namespace Skyra.Commands.Arguments
{
	[Resolver(typeof(int))]
	public class Int32Resolver
	{
		public Task<int> ResolveAsync(Message message, string content)
		{
			var resolved = Int32.Parse(content);
			return Task.FromResult(resolved);
		}
	}
}
