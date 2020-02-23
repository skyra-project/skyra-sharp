using System;
using System.Threading.Tasks;
using Skyra.Core;
using Skyra.Core.Structures.Attributes;
using Spectacles.NET.Types;

namespace Skyra.Arguments
{
	[Resolver(typeof(DateTime), "date")]
	public class DateTimeResolver
	{
		private readonly Client _client;

		public DateTimeResolver(Client client)
		{
			_client = client;
		}

		public Task<DateTime> ResolveAsync(Message message, string content)
		{
			var resolved = DateTime.Parse(content);
			return Task.FromResult(resolved);
		}
	}
}
