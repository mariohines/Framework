using System.Data.SqlClient;
using FluentAssertions;
using Framework.Core.Configuration;
using NUnit.Framework;

namespace Framework.Core.Tests.Configuration
{
	[TestFixture]
	public class DynamicConfigurationManagerTests
	{
		[Test]
		public void CanGetAppSettings() {
			// arrange
			var expectedResult = "One";
			
			// act
			string actualResult = DynamicConfigurationManager.AppSettings.Test;

			// assert
			actualResult.Should().Be(expectedResult);
		}

		[Test]
		public void CanGetConnectionStrings() {
			// arrange
			var expectedResultObject = new SqlConnectionStringBuilder("data source=localhost; initial catalog=master; integrated security=true;");

			// act
			string actualResult = DynamicConfigurationManager.ConnectionStrings.Test;
			var actualResultObject = new SqlConnectionStringBuilder(actualResult);

			// assert
			actualResultObject.DataSource.Should().Be(expectedResultObject.DataSource);
			actualResultObject.IntegratedSecurity.Should().Be(expectedResultObject.IntegratedSecurity);
			actualResultObject.InitialCatalog.Should().Be(expectedResultObject.InitialCatalog);
		}
	}
}