using NUnit.Framework;
using Skyra.Core.Utils;

namespace Skyra.Tests.Utils
{
	public sealed class InspectionFormatterTests
	{
		[Test]
		public void InspectionFormatter_Formats_BoolTrue()
		{
			var formatter = new InspectionFormatter(true);
			Assert.Equals(formatter.ToString(), "true");
		}

		[Test]
		public void InspectionFormatter_Formats_BoolFalse()
		{
			var formatter = new InspectionFormatter(false);
			Assert.Equals(formatter.ToString(), "false");
		}

		[Test]
		public void InspectionFormatter_Formats_BasicString()
		{
			var formatter = new InspectionFormatter("Hello world");
			Assert.Equals(formatter.ToString(), @"""Hello world""");
		}

		[Test]
		public void InspectionFormatter_Formats_QuotedString()
		{
			var formatter = new InspectionFormatter(@"Hello ""world""");
			Assert.Equals(formatter.ToString(), @"""Hello \""world""");
		}
	}
}
