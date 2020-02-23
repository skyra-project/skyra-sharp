using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(DateTime), "date")]
	public class DateTimeResolver : StructureBase
	{
		public DateTimeResolver(Client client) : base(client)
		{
		}

		public Task<DateTime> ResolveAsync(Message message, string content)
		{
			var resolved = DateTime.Parse(content);
			return Task.FromResult(resolved);
		}
	}
}
