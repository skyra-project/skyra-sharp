using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Skyra.Core;
using Skyra.Core.Cache.Models;
using Skyra.Core.Structures;
using Skyra.Core.Structures.Attributes;
using Skyra.Core.Structures.Usage;

namespace Skyra.Worker.Resolvers
{
	[Resolver(typeof(string), "string")]
	public class StringResolver : StructureBase
	{
		public StringResolver(IClient client) : base(client)
		{
		}

		[NotNull]
		public Task<string> ResolveAsync(CoreMessage message, CommandUsageOverloadArgument argument,
			[CanBeNull] string content)
		{
			if (string.IsNullOrEmpty(content))
			{
				return Task.FromException<string>(new ArgumentException("Gimme a string!"));
			}

			if (content.Length < argument.Minimum)
			{
				return Task.FromException<string>(new ArgumentException(
					$"{content} is too short, you must give at least {argument.Minimum.ToString()} characters."));
			}

			if (content.Length > argument.Maximum)
			{
				return Task.FromException<string>(new ArgumentException(
					$"{content} is too long, you must give less than {argument.Maximum.ToString()} characters."));
			}

			return Task.FromResult(content);
		}
	}
}
