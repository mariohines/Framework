using System.Collections.Generic;
using System.Collections.Specialized;
using FluentAssertions;
using Framework.Collections;
using NUnit.Framework;

namespace Framework.Tests.Collections
{
	[TestFixture]
	public class DynamicCollectionTests
	{
		private static readonly object[] CanGetValueSource =
		{
			new object[] {new NameValueCollection {{"Get", "Some"}}, "Some"}
		};

		private static readonly object[] CanUseDictionarySource =
		{
			new object[] {new Dictionary<string, object> {{"Get", "Some"}}, "Some"},
			new object[] {new Dictionary<string, object> {{"Get", 1}}, 1},
			new object[] {new Dictionary<string, object> {{"Get", .01}}, .01}
		};

		[TestCaseSource("CanGetValueSource")]
		public void CanGetValue(NameValueCollection input, string expectedResult) {
			// arrange
			dynamic collection = new DynamicCollection(input);

			// act
			string actualResult = collection.Get;

			// assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("Some")]
		[TestCase(1)]
		[TestCase(.001)]
		public void CanSetValue(object expectedResult) {
			// arrange
			dynamic collection = new DynamicCollection();

			// act
			collection.Get = expectedResult;
			object actualResult = collection.Get;

			// assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCaseSource("CanUseDictionarySource")]
		public void CanUseDictionary(IDictionary<string, object> input, object expectedResult) {
			// arrange
			dynamic collection = new DynamicCollection(input);

			// act
			object actualResult = collection.Get;

			// assert
			actualResult.Should().Be(expectedResult);
		}
	}
}
