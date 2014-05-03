using Framework.Core.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Framework.Core.Tests.Interfaces
{
	[TestFixture]
	public class DependencyInjectorTests
	{
		private IDependencyInjector _injector;

		[SetUp]
		public void TestSetup() {
			_injector = Substitute.For<IDependencyInjector>();
		}


	}
}