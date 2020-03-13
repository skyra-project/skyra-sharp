using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Resolvers
{
	[Resolver(typeof(int), "integer")]
	public class Int32Resolver : StructureBase
	{
		public Int32Resolver(Client client) : base(client)
		{
		}

		public Task<int> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument, string content)
		{
			if (!int.TryParse(content, out var resolved))
			{
				throw new ArgumentException($"I could not resolve a number from {content}");
			}

			if (resolved < argument.Minimum)
			{
				throw new ArgumentException(
					$"{resolved.ToString()} is too big, you must give a number bigger or equals than {argument.Minimum}.");
			}

			if (resolved > argument.Maximum)
			{
				throw new ArgumentException(
					$"{resolved.ToString()} is too small, you must give a number smaller or equals than {argument.Maximum}.");
			}

			return Task.FromResult(resolved);
		}
	}
}
