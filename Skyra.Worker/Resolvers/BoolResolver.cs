using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Worker.Resolvers
{
	[Resolver(typeof(bool), "boolean")]
	public sealed class BoolResolver : StructureBase
	{
		private static readonly string[] Truths = {"1", "t", "true", "+", "y", "yes"};
		private static readonly string[] Falses = {"0", "f", "false", "-", "n", "no"};

		public BoolResolver(IClient client) : base(client)
		{
		}

		[NotNull]
		public Task<bool> ResolveAsync(Message message, CommandUsageOverloadArgument argument,
			[NotNull] string content)
		{
			var boolean = content.ToLower();
			return Truths.Contains(boolean)
				? Task.FromResult(true)
				: Falses.Contains(boolean)
					? Task.FromResult(false)
					: Task.FromException<bool>(new ArgumentException("Gimme a valid boolean!"));
		}
	}
}
