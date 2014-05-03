using System.Xml;
using FluentAssertions;
using Framework.Core.Builders;
using Framework.Core.Interfaces;
using Framework.Core.Tests.TestObjects;
using NUnit.Framework;

namespace Framework.Core.Tests.Builders
{
	[TestFixture]
	public class XmlClassBuilderTests
	{
		private IXmlSerializer _serializer;

		private static readonly object[] ToXmlStringSource =
		{
			new object[]
			{
				new TestObject {Id = 1, Message = "Test"},
				"<?xml version=\"1.0\" encoding=\"utf-16\"?><TestObject><Id>1</Id><Message>Test</Message></TestObject>"
			}
		};

		[TestFixtureSetUp]
		public void Setup() {
			_serializer = new XmlClassBuilder(canIndent: false, lineHandling: NewLineHandling.None);
		}
		
		[TestCaseSource("ToXmlStringSource")]
		public void ToXmlStringTest(TestObject input, string expectedResult) {
			// act
			var actualResult = _serializer.ToXmlString(input);

			// assert
			actualResult.Should().Be(expectedResult);
		}
	}
}
