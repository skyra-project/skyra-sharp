using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Spectacles.NET.Types;

namespace Skyra.Resolvers
{
	[Resolver(typeof(string), "string")]
	public class StringResolver : StructureBase
	{
		public StringResolver(Client client) : base(client)
		{
		}

		public Task<string> ResolveAsync(Message message, CommandUsageOverloadArgument argument, string content)
		{
			if (string.IsNullOrEmpty(content)) throw new Exception("Gimme a string!");
			return Task.FromResult(content);
		}
	}
}
