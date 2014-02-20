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
			string result = collection.Get;

			// assert
			result.Should().Be(expectedResult);
		}

		[Test]
		public void CanSetValue() {
			// arrange
			dynamic collection = new DynamicCollection();

			// act
			collection.Get = "Some";
			string result = collection.Get;

			// assert
			result.Should().Be("Some");
		}

		[TestCaseSource("CanUseDictionarySource")]
		public void CanUseDictionary(IDictionary<string, object> input, object expectedResult) {
			// arrange
			dynamic collection = new DynamicCollection(input);

			// act
			object result = collection.Get;

			// assert
			result.Should().Be(expectedResult);
		}
	}
}
