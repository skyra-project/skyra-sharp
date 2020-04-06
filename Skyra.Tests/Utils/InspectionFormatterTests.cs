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
			Assert.AreEqual("true", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_BoolFalse()
		{
			var formatter = new InspectionFormatter(false);
			Assert.AreEqual("false", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_LetterChar()
		{
			var formatter = new InspectionFormatter('a');
			Assert.AreEqual("'a'", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_SymbolChar()
		{
			var formatter = new InspectionFormatter('$');
			Assert.AreEqual("'$'", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_UnicodeChar()
		{
			var formatter = new InspectionFormatter('ア');
			Assert.AreEqual("'ア'", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ControlChar()
		{
			var formatter = new InspectionFormatter('\u0000');
			Assert.AreEqual("'\\u0000'", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_UnicodePart()
		{
			var formatter = new InspectionFormatter("😊"[0]);
			Assert.AreEqual("'\\uD83D'", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_BasicString()
		{
			var formatter = new InspectionFormatter("Hello world");
			Assert.AreEqual(@"""Hello world""", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_QuotedString()
		{
			var formatter = new InspectionFormatter(@"Hello ""world""");
			Assert.AreEqual(@"""Hello \""world\""""", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_SignedByte()
		{
			var formatter = new InspectionFormatter((sbyte) 0x14);
			Assert.AreEqual("0x14", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_SignedBytePadded()
		{
			var formatter = new InspectionFormatter((sbyte) 0x04);
			Assert.AreEqual("0x04", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Byte()
		{
			var formatter = new InspectionFormatter((byte) 0x14);
			Assert.AreEqual("0x14U", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_BytePadded()
		{
			var formatter = new InspectionFormatter((byte) 0x04);
			Assert.AreEqual("0x04U", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Short()
		{
			var formatter = new InspectionFormatter((short) 256);
			Assert.AreEqual("256", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_UnsignedShort()
		{
			var formatter = new InspectionFormatter((ushort) 256);
			Assert.AreEqual("256U", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_Int()
		{
			var formatter = new InspectionFormatter(5500000);
			Assert.AreEqual("5500000", formatter.ToString());
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
		public void InspectionFormatter_Formats_ArrayOfIntsNoDepth()
		{
			var formatter = new InspectionFormatter(new[] {4, 6, 3, 7}, 0);
			Assert.AreEqual("Int32[4]", formatter.ToString());
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
		public void InspectionFormatter_Formats_DictionaryEmptyNoDepth()
		{
			var dictionary = new Dictionary<char, string>();
			var formatter = new InspectionFormatter(dictionary, 0);
			Assert.AreEqual("[Dictionary<Char, String>]", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_DictionaryEmpty()
		{
			var dictionary = new Dictionary<char, string>();
			var formatter = new InspectionFormatter(dictionary);
			Assert.AreEqual("Dictionary<Char, String> {}", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_DictionaryPopulated()
		{
			var dictionary = new Dictionary<char, string> {{'a', "An amazing \"flute\"!"}, {'b', "Boom!"}};
			var formatter = new InspectionFormatter(dictionary);
			Assert.AreEqual(@"Dictionary<Char, String> {
  'a' => ""An amazing \""flute\""!"",
  'b' => ""Boom!"" }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ExtendedDictionaryEmpty()
		{
			var dictionary = new ExtendedDictionaryTest();
			var formatter = new InspectionFormatter(dictionary);
			Assert.AreEqual(@"ExtendedDictionaryTest [Dictionary] {}", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ExtendedDictionaryPopulated()
		{
			var dictionary = new ExtendedDictionaryTest {{1, 4}, {6, 12}};
			var formatter = new InspectionFormatter(dictionary);
			Assert.AreEqual(@"ExtendedDictionaryTest [Dictionary] {
  1 => 4,
  6 => 12 }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_EmptyStruct()
		{
			var formatter = new InspectionFormatter(new TestEmptyStruct());
			Assert.AreEqual("TestEmptyStruct {}", formatter.ToString());
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

		[Test]
		public void InspectionFormatter_Formats_Class()
		{
			var formatter = new InspectionFormatter(new ReferenceTest());
			Assert.AreEqual(@"ReferenceTest {
  Pointer: null }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ClassReference()
		{
			var data = new ReferenceTest {Pointer = new ReferenceTest()};
			var formatter = new InspectionFormatter(data);
			Assert.AreEqual(@"ReferenceTest {
  Pointer: [ReferenceTest] }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_ClassCircular()
		{
			var data = new ReferenceTest();
			data.Pointer = data;
			var formatter = new InspectionFormatter(data);
			Assert.AreEqual(@"ReferenceTest {
  Pointer: [Circular] }", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_DelegateSingleArgument()
		{
			var formatter = new InspectionFormatter(new DelegateSingleArgument(name => name));
			Assert.AreEqual("DelegateSingleArgument(String name)", formatter.ToString());
		}

		[Test]
		public void InspectionFormatter_Formats_DelegateMultipleArguments()
		{
			var formatter = new InspectionFormatter(new DelegateMultipleArguments((a, b) => a + b));
			Assert.AreEqual("DelegateMultipleArguments(Int32 a, Int32 b)", formatter.ToString());
		}

		private delegate string DelegateSingleArgument(string name);
		private delegate int DelegateMultipleArguments(int a, int b);
	}

	internal struct TestEmptyStruct
	{
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

	internal sealed class ReferenceTest
	{
		public ReferenceTest? Pointer { get; set; }
	}

	internal sealed class ExtendedDictionaryTest : Dictionary<int, int>
	{
	}
}
