using System.Text.RegularExpressions;
using JetBrains.Annotations;
using NUnit.Framework;
using Skyra.Core.Utils;

namespace Skyra.Tests.Core.Utils
{
	public sealed class StringExtensionsTests
	{
		[Test]
		public void StringExtension_Replace_NoMatches(
			[Values(
				"abc",
				"$@~",
				"ウメリカ"
			)]
			[NotNull]
			string value)
		{
			var regex = new Regex(@"\d");
			var result = value.Replace(regex, groups => groups[0].Value + groups[0].Value);
			Assert.AreEqual(value, result);
		}

		[Test]
		public void StringExtension_Replace_Matches()
		{
			var regex = new Regex(@"\d");
			var result = "a52c".Replace(regex, groups => groups[0].Value + groups[0].Value);
			Assert.AreEqual("a5522c", result);
		}

		[Test]
		public void StringExtension_Replace_MatchesMultiple()
		{
			var regex = new Regex(@"\d+");
			var result = "a52c".Replace(regex, groups => groups[0].Value + groups[0].Value);
			Assert.AreEqual("a5252c", result);
		}

		[Test]
		public void StringExtension_Replace_MatchesLast()
		{
			var regex = new Regex(@"\d");
			var result = "a52".Replace(regex, groups => groups[0].Value + groups[0].Value);
			Assert.AreEqual("a5522", result);
		}

		[Test]
		public void StringExtension_EscapeRegexPatterns()
		{
			var escaped = @"[\d]".EscapeRegexPatterns();
			Assert.AreEqual(@"\[\\d\]", escaped);
		}
	}
}
