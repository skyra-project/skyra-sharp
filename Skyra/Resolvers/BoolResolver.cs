using System;
using System.Linq;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(bool), "boolean")]
	public class BoolResolver : StructureBase
	{
		private static readonly string[] Truths = {"1", "t", "true", "+", "y", "yes"};
		private static readonly string[] Falses = {"0", "f", "false", "-", "n", "no"};

		public BoolResolver(Client client) : base(client)
		{
		}

		public Task<bool> ResolveAsync(Message message, CommandUsageOverloadArgument argument, string content)
		{
			var boolean = content.ToLower();
			if (Truths.Contains(boolean)) return Task.FromResult(true);
			if (Falses.Contains(boolean)) return Task.FromResult(false);
			throw new Exception("Gimme a valid boolean!");
		}
	}
}
