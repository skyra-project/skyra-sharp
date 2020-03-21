using System.Threading.Tasks;
using NUnit.Framework;
using Skyra.Resolvers;

#pragma warning disable 8625

namespace Skyra.Tests.Resolvers
{
	public sealed class ResolverTests
	{
		[Test]
		public async Task BoolResolver_Resolves_TruthfulBools(
			[Values("1", "t", "true", "+", "y", "yes")]
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
