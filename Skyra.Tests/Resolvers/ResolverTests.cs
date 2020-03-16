using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Skyra.Resolvers;

namespace Skyra.Tests.Resolvers
{
	public class ResolverTests
	{
		[Test]
		public async Task BoolResolver_Resolves_TruthfulBools(
			[Values("1", "t", "true", "+", "y", "yes")] string argument)
		{
			// assign
			var resolver = new BoolResolver(null);

			// act
			var result = await resolver.ResolveAsync(null, null, argument);

			// assert
			Assert.IsTrue(result);
		}
	}
}
