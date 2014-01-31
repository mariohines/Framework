using System.Collections.Generic;
using System.Linq;
using Framework.Data.Comparers;
using Framework.Data.Tests.TestObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Framework.Data.Tests.Comparers
{
	[TestFixture]
	public class DataPropertyComparerTests
	{
		private TestableObjectOne _testable1;
		private TestableObjectOne _testable2;

		[TestFixtureSetUp]
		public void Setup() {
			_testable1 = new TestableObjectOne {
				Id = 1,
				IsValid = true,
				Message = "Test"
			};
			_testable2 = new TestableObjectOne {
				Id = 1,
				IsValid = true,
				Message = "test"
			};
		}

		[TestFixtureTearDown]
		public void Cleanup() {
			_testable1 = null;
			_testable2 = null;
		}

		[Test]
		public void EqualityTestWithIdAndMessage() {
			//arrange
			var comparer = new DataPropertyComparer<TestableObjectOne>("Id", "Message");

			//act
			var actualResult = comparer.Equals(_testable1, _testable2);

			//assert
			actualResult.Should().BeFalse();
		}

		[Test]
		public void EqualityTestWithIdAndIsValid() {
			//arrange
			var comparer = new DataPropertyComparer<TestableObjectOne>("Id", "IsValid");

			//act
			var actualResult = comparer.Equals(_testable1, _testable2);

			//assert
			actualResult.Should().BeTrue();
		}

		[Test]
		public void LinqDistinctTestWithAllProperties() {
			//arrange
			var list = new List<TestableObjectOne> {
				new TestableObjectOne {Id = 1, IsValid = false, Message = "Test"},
				new TestableObjectOne {Id = 1, IsValid = false, Message = "test"},
				new TestableObjectOne {Id = 1, IsValid = true, Message = "Test"},
				new TestableObjectOne {Id = 1, IsValid = true, Message = "Test"}
			};

			//act
			var distinctList = list.Distinct(new DataPropertyComparer<TestableObjectOne>("Id", "IsValid", "Message")).ToList();

			//assert
			distinctList.Count.Should().Be(3);
		}
	}
}