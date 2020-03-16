using System;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Resolvers
{
	[Resolver(typeof(bool), "boolean")]
	public class BoolResolver : StructureBase
	{
		private static readonly string[] Truths = {"1", "t", "true", "+", "y", "yes"};
		private static readonly string[] Falses = {"0", "f", "false", "-", "n", "no"};

		public BoolResolver(Client client) : base(client)
		{
		}

		public Task<bool> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument, string content)
		{
			var boolean = content.ToLower();
			if (Truths.Contains(boolean)) return Task.FromResult(true);
			if (Falses.Contains(boolean)) return Task.FromResult(false);
			return Task.FromException<bool>(new ArgumentException("Gimme a valid boolean!"));
		}
	}
}
