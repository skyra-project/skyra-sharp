using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Resolvers
{
	[Resolver(typeof(DateTime), "date")]
	public class DateTimeResolver : StructureBase
	{
		public DateTimeResolver(IClient client) : base(client)
		{
		}

		[NotNull]
		public Task<DateTime> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument, string content)
		{
			return DateTime.TryParse(content, out var resolved)
				? Task.FromResult(resolved)
				: Task.FromException<DateTime>(new ArgumentException($"I could not resolve a date from {content}"));
		}
	}
}
