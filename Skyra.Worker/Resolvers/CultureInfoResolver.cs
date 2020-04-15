using System;
using System.Globalization;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Worker.Resolvers
{
	[Resolver(typeof(CultureInfo), "language")]
	public class CultureInfoResolver : StructureBase
	{
		public CultureInfoResolver(IClient client) : base(client)
		{
		}

		[NotNull]
		public Task<CultureInfo> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument,
			[NotNull] string content)
		{
			if (Client.Cultures.TryGetValue(content, out var resolved)) return Task.FromResult(resolved);
			return Task.FromException<CultureInfo>(
				new ArgumentException($"I could not resolve a language from {content}"));
		}
	}
}
