using System;
using System.Collections.Generic;
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
			Assert.AreEqual(formatter.ToString(), "true");
		}

		[Test]
		public void InspectionFormatter_Formats_BoolFalse()
		{
			var formatter = new InspectionFormatter(false);
			Assert.AreEqual(formatter.ToString(), "false");
		}

		[Test]
		public void InspectionFormatter_Formats_LetterChar()
		{
			var formatter = new InspectionFormatter('a');
			Assert.AreEqual(formatter.ToString(), "'a'");
		}

		[Test]
		public void InspectionFormatter_Formats_SymbolChar()
		{
			var formatter = new InspectionFormatter('$');
			Assert.AreEqual(formatter.ToString(), "'$'");
		}

		[Test]
		public void InspectionFormatter_Formats_UnicodeChar()
		{
			var formatter = new InspectionFormatter('ア');
			Assert.AreEqual(formatter.ToString(), "'ア'");
		}

		[Test]
		public void InspectionFormatter_Formats_ControlChar()
		{
			var formatter = new InspectionFormatter('\u0000');
			Assert.AreEqual(formatter.ToString(), "'\\u0000'");
		}

		[Test]
		public void InspectionFormatter_Formats_UnicodePart()
		{
			var formatter = new InspectionFormatter("😊"[0]);
			Assert.AreEqual(formatter.ToString(), "'\\uD83D'");
		}

		[Test]
		public void InspectionFormatter_Formats_BasicString()
		{
			var formatter = new InspectionFormatter("Hello world");
			Assert.AreEqual(formatter.ToString(), @"""Hello world""");
		}

		[Test]
		public void InspectionFormatter_Formats_QuotedString()
		{
			var formatter = new InspectionFormatter(@"Hello ""world""");
			Assert.AreEqual(formatter.ToString(), @"""Hello \""world\""""");
		}

		[Test]
		public void InspectionFormatter_Formats_SignedByte()
		{
			var formatter = new InspectionFormatter((sbyte) 0x14);
			Assert.AreEqual(formatter.ToString(), "0x14");
		}

		[Test]
		public void InspectionFormatter_Formats_SignedBytePadded()
		{
			var formatter = new InspectionFormatter((sbyte) 0x04);
			Assert.AreEqual(formatter.ToString(), "0x04");
		}

		[Test]
		public void InspectionFormatter_Formats_Byte()
		{
			var formatter = new InspectionFormatter((byte) 0x14);
			Assert.AreEqual(formatter.ToString(), "0x14U");
		}

		[Test]
		public void InspectionFormatter_Formats_BytePadded()
		{
			var formatter = new InspectionFormatter((byte) 0x04);
			Assert.AreEqual(formatter.ToString(), "0x04U");
		}

		[Test]
		public void InspectionFormatter_Formats_Short()
		{
			var formatter = new InspectionFormatter((short) 256);
			Assert.AreEqual(formatter.ToString(), "256");
		}

		[Test]
		public void InspectionFormatter_Formats_UnsignedShort()
		{
			var formatter = new InspectionFormatter((ushort) 256);
			Assert.AreEqual(formatter.ToString(), "256U");
		}

		[Test]
		public void InspectionFormatter_Formats_Int()
		{
			var formatter = new InspectionFormatter(5500000);
			Assert.AreEqual(formatter.ToString(), "5500000");
		}

		[Test]
		public void InspectionFormatter_Formats_UnsignedInt()
		{
			var formatter = new InspectionFormatter(5500000U);
			Assert.AreEqual("5500000U", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Long()
		{
			var formatter = new InspectionFormatter(242043489611808769L);
			Assert.AreEqual("242043489611808769L", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_UnsignedLong()
		{
			var formatter = new InspectionFormatter(242043489611808769UL);
			Assert.AreEqual("242043489611808769UL", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Float()
		{
			var formatter = new InspectionFormatter(0.2f);
			Assert.AreEqual("0.2F", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Double()
		{
			var formatter = new InspectionFormatter(0.123456D);
			Assert.AreEqual("0.123456D", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Decimal()
		{
			var formatter = new InspectionFormatter(0.1234567891234M);
			Assert.AreEqual("0.1234567891234M", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_DateTime()
		{
			var formatter = new InspectionFormatter(DateTime.Parse("2020-04-06T16:03:45.040Z"));
			Assert.AreEqual("2020-04-06T16:03:45.040Z", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_TimeSpan()
		{
			var formatter = new InspectionFormatter(TimeSpan.FromMinutes(5));
			Assert.AreEqual("00:05:00", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Type()
		{
			var formatter = new InspectionFormatter('a'.GetType());
			Assert.AreEqual("System.Char", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ArrayOfInts()
		{
			var formatter = new InspectionFormatter(new[] {4, 6, 3, 7});
			Assert.AreEqual("Int32[4] { 4, 6, 3, 7 }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ArrayOfChars()
		{
			var formatter = new InspectionFormatter(new[] {'a', 'ア', '$', '\u0000'});
			Assert.AreEqual("Char[4] { 'a', 'ア', '$', '\\u0000' }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Dictionary()
		{
			var dictionary = new Dictionary<char, string> {{'a', "An amazing \"flute\"!"}, {'b', "Boom!"}};
			var formatter = new InspectionFormatter(dictionary);
			Assert.AreEqual(@"Dictionary`2<Char, String> {
  'a' => ""An amazing \""flute\""!"",
  'b' => ""Boom!"" }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Struct()
		{
			var data = new TestStruct {Char = 'a', Code = 97};
			var formatter = new InspectionFormatter(data);
			Assert.AreEqual(@"TestStruct {
  Char: 'a',
  Code: 0x61 }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_NoDepthStruct()
		{
			var data = new InnerTestStruct
			{
				Name = "LowerCaseCharacter", Identifier = IdentifierType.LowerCase,
				Data = new TestStruct {Char = 'a', Code = 97}
			};
			var formatter = new InspectionFormatter(data);
			Assert.AreEqual(@"InnerTestStruct {
  Identifier: IdentifierType.LowerCase,
  Name: ""LowerCaseCharacter"",
  Data: [TestStruct] }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_DepthStruct()
		{
			var data = new InnerTestStruct
			{
				Name = "LowerCaseCharacter", Identifier = IdentifierType.LowerCase,
				Data = new TestStruct {Char = 'a', Code = 97}
			};
			var formatter = new InspectionFormatter(data, 2);
			Assert.AreEqual(@"InnerTestStruct {
  Identifier: IdentifierType.LowerCase,
  Name: ""LowerCaseCharacter"",
  Data: TestStruct {
    Char: 'a',
    Code: 0x61 } }", formatter.ToString());
		}
	}

	internal struct TestStruct
	{
		public char Char { get; set; }
		public sbyte Code { get; set; }
	}

	internal enum IdentifierType
	{
		LowerCase
	}

	internal struct InnerTestStruct
	{
		public IdentifierType Identifier { get; set; }
		public string Name { get; set; }
		public TestStruct Data { get; set; }
	}
}
