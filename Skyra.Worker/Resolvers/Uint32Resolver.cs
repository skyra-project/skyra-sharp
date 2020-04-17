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
	[Resolver(typeof(uint), "integer")]
	public class Uint32Resolver : StructureBase
	{
		public Uint32Resolver(IClient client) : base(client)
		{
		}

		[NotNull]
		public Task<uint> ResolveAsync(Message message, CommandUsageOverloadArgument argument, string content)
		{
			if (!uint.TryParse(content, out var resolved))
			{
				return Task.FromException<uint>(new ArgumentException($"I could not resolve a number from {content}"));
			}

			if (resolved < argument.Minimum)
			{
				return Task.FromException<uint>(new ArgumentException(
					$"{resolved.ToString()} is too small, you must give a number bigger or equals than {argument.Minimum.ToString()}."));
			}

			if (resolved > argument.Maximum)
			{
				return Task.FromException<uint>(new ArgumentException(
					$"{resolved.ToString()} is too big, you must give a number smaller or equals than {argument.Maximum.ToString()}."));
			}

			return Task.FromResult(resolved);
		}
	}
}
