using System.Threading.Tasks;
using JetBrains.Annotations;
using NUnit.Framework;
using Skyra.Worker.Resolvers;

#pragma warning disable 8625

namespace Skyra.Tests.Resolvers
{
	public sealed class ResolverTests
	{
		[Test]
		public async Task BoolResolver_Resolves_TruthfulBools(
			[Values("1", "t", "true", "+", "y", "yes")] [NotNull]
			string argument)
		{
			// Assign
			var resolver = new BoolResolver(null);

			// Act
			var result = await resolver.ResolveAsync(null, null, argument);

			// Assert
			Assert.IsTrue(result);
		}
	}
}
