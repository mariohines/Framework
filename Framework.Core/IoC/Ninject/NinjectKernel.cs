using System;
using System.Threading;
using Ninject;

namespace Framework.Core.IoC.Ninject
{
	/// <summary>IOC kernel.</summary>
	/// <remarks>A singleton of the standard kernel for Ninject.</remarks>
	public sealed class NinjectKernel
	{
		/// <summary>The kernel.</summary>
		private static readonly Lazy<IKernel> Kernel;

		/// <summary>Gets the instance.</summary>
		/// <value>The instance.</value>
		public static IKernel Instance { get { return Kernel.Value; } }

		/// <summary>Static constructor.</summary>
		static NinjectKernel() {
			Kernel = new Lazy<IKernel>(() => new StandardKernel(), LazyThreadSafetyMode.PublicationOnly);
		}
	}
}
