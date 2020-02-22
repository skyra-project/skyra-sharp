using System;
using System.Threading.Tasks;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(string))]
	public class StringResolver
	{
		public Task<string> ResolveAsync(Message _, string content)
		{
			if (string.IsNullOrEmpty(content)) throw new Exception("Gimme a string!");
			return Task.FromResult(content);
		}
	}
}
