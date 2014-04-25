using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Framework.Core.Enumerations;
using Framework.Core.Extensions;
using Framework.Tests.TestObjects;
using NUnit.Framework;

namespace Framework.Tests.Extensions
{
	[TestFixture]
	public class ExtensionsTests
	{
		[TestCase("1", false)]
		[TestCase(null, true)]
		[TestCase(new object[] {}, false)]
		public void IsNullTest(object value, bool expectedResult) {
			//act
			var actualResult = value.IsNull();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("1")]
		[TestCase(null, ExpectedException = typeof (ArgumentNullException))]
		public void ThrowIfNullTest(object value) {
			//act
			value.ThrowIfNull();

			//assert
			value.Should().NotBeNull();
		}

		[TestCase("08/07/2013", false)]
		[TestCase("08/10/2013", true)]
		public void IsWeekendTest(string dateTime, bool expectedResult) {
			//arrange
			var value = Convert.ToDateTime(dateTime);

			//act
			var actualResult = value.IsWeekend();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("08/15/2013", "08/01/2013")]
		[TestCase("6/02/2013", "6/01/2013")]
		public void FirstDayTest(string dateTime, string expectedDateTime) {
			//arrange
			var value = Convert.ToDateTime(dateTime);
			var expectedResult = Convert.ToDateTime(expectedDateTime);

			//act
			var actualResult = value.FirstDay();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("08/15/2013", "08/31/2013")]
		[TestCase("2/01/2013", "2/28/2013", Description = "Leap Year Test for non-Leap year.")]
		[TestCase("2/01/2012", "2/29/2012", Description = "Leap Year Test for leap year.")]
		public void LastDayTest(string dateTime, string expectedDateTime) {
			//arrange
			var value = Convert.ToDateTime(dateTime);
			var expectedResult = Convert.ToDateTime(expectedDateTime);

			//act
			var actualResult = value.LastDay();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("5/01/2013", Month.May)]
		[TestCase("1/1/2013", Month.January)]
		public void GetMonthTest(string dateTime, Month expectedResult) {
			//arrange
			var value = Convert.ToDateTime(dateTime);

			//act
			var actualResult = value.GetMonth();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("9/1/2013", ZodiacSign.Virgo)]
		[TestCase("1/1/2013", ZodiacSign.Capricorn)]
		public void ResolveZodiacTest(string dateTime, ZodiacSign expectedResult) {
			//arrange
			var value = Convert.ToDateTime(dateTime);

			//act
			var actualResult = value.ResolveZodiac();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase(new[] {"cat", "hat", "dog"}, "the ", new[] {"the cat", "the hat", "the dog"}, true)]
		[TestCase(new[] {"cat", "hat", "dog"}, "the", new[] {"the cat", "the hat", "the dog"}, false)]
		public void PrefixAllTest(IEnumerable<string> values, string prefix, IEnumerable<string> compareValues, bool expectedResult) {
			//act
			var actualResult = values.PrefixAll(prefix);

			//assert
			actualResult.SequenceEqual(compareValues).Should().Be(expectedResult);
		}

		[TestCase(new[] { "cat", "hat", "dog" }, " runs", new[] { "cat runs", "hat runs", "dog runs" }, true)]
		[TestCase(new[] { "cat", "hat", "dog" }, "runs", new[] { "cat runs", "hat runs", "dog runs" }, false)]
		public void SuffixAllTest(IEnumerable<string> values, string suffix, IEnumerable<string> compareValues, bool expectedResult) {
			//act
			var actualResult = values.SuffixAll(suffix);

			//assert
			actualResult.SequenceEqual(compareValues).Should().Be(expectedResult);
		}

		[TestCase(new[] {"The", "cat", "in", "the", "hat"}, " ", "The cat in the hat")]
		[TestCase(new[] { "The", "cat", "in", "the", "hat" }, ",", "The,cat,in,the,hat")]
		[TestCase(new[] { "The", "cat", "in", "the", "hat" }, ".", "The.cat.in.the.hat")]
		public void DelimitAllToString(IEnumerable<string> values, string delimiter, string expectedResult) {
			//act
			var actualResult = values.DelimitAllToString(delimiter);

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase(null, new string[] {})]
		public void EmptyIfNullTest(IEnumerable<string> value, IEnumerable<string> expectedResult) {
			//act
			var actualResult = value.EmptyIfNull();

			//assert
			actualResult.Should().BeEquivalentTo(expectedResult);
		}

		[TestCase(new[] {"1", "2", "3", "4"}, new[] {1, 2, 3, 4})]
		public void SafeCastTest(IEnumerable<string> values, IEnumerable<int> expectedResult) {
			//act
			var actualResult = values.SafeCast<int>();

			//assert
			actualResult.SequenceEqual(expectedResult).Should().BeTrue();
		}

		[TestCase(null, true)]
		[TestCase(new[] {"1", "2"}, false)]
		[TestCase(new object[] {}, true)]
		public void IsEmptyOrNullTest(IEnumerable<object> value, bool expectedResult) {
			//act
			var actualResult = value.IsEmptyOrNull();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("mario hines", "Mario Hines")]
		public void ToProperCaseTest(string value, string expectedResult) {
			//act
			var actualResult = value.ToProperCase();

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase("This is {0}.", "This is you.", new[] { "you" })]
		[TestCase("This is {0} {1}.", "This is you again.", new[] { "you", "again" })]
		[TestCase("This is {0} {1} {2}.", "This is you again here.", new[] { "you", "again", "here" })]
		[TestCase("This is {0} {1} {2} {3}.", "This is you again here later.", new[] { "you", "again", "here", "later" })]
		[TestCase("This is {0} {1} {2} {3} {4}.", "This is how we roll 100 percent.", new[] { "how", "we", "roll", "100", "percent" })]
		[TestCase("This is {0} {1} {2} {3} {4} {5}.", "This is how we roll 100 percent BOY.", new[] { "how", "we", "roll", "100", "percent", "BOY" })]
		public void FormatWithTest(string value, string expectedResult, params object[] parameters) {
			//act
			var actualResult = value.FormatWith(parameters);

			//assert
			actualResult.Should().Be(expectedResult);
		}

		[TestCase(TestEnumOne.Bird, "Bird")]
		[TestCase(TestEnumTwo.Mario, "Mario Hines")]
		public void GetDisplayTests(Enum input, string expectedResult) {
			//act
			var actualResult = input.GetDisplay();

			//assert
			actualResult.Should().Be(expectedResult);
		}
	}
}