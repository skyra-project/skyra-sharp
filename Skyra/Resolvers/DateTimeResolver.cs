using System;
using System.Threading.Tasks;
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
		public DateTimeResolver(Client client) : base(client)
		{
		}

		public Task<DateTime> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument, string content)
		{
			var resolved = DateTime.Parse(content);
			return Task.FromResult(resolved);
		}
	}
}
