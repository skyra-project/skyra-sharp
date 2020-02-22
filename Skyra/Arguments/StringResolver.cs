using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(string), "string")]
	public class StringResolver
	{
		private readonly Client _client;

		public StringResolver(Client client)
		{
			_client = client;
		}

		public Task<string> ResolveAsync(Message _, string content)
		{
			if (string.IsNullOrEmpty(content)) throw new Exception("Gimme a string!");
			return Task.FromResult(content);
		}
	}
}
