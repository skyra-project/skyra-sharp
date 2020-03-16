using System;
using System.Globalization;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Resolvers
{
	[Resolver(typeof(CultureInfo), "language")]
	public class CultureInfoResolver : StructureBase
	{
		public CultureInfoResolver(Client client) : base(client)
		{
		}

		public Task<CultureInfo> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument,
			string content)
		{
			if (Client.Cultures.TryGetValue(content, out var resolved)) return Task.FromResult(resolved);
			return Task.FromException<CultureInfo>(
				new ArgumentException($"I could not resolve a language from {content}"));
		}
	}
}
