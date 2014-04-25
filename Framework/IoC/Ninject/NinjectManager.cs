using System;
using Ninject;
using Ninject.Parameters;

namespace Framework.Core.IoC.Ninject
{
	/// <summary>Manager for Inversion of Control containers.</summary>
	/// <remarks>A manager for the Ninject framework and handling bindings.</remarks>
	public sealed class NinjectManager
	{
		private static IKernel _kernel;
		///<summary>Gets or sets the kernel.</summary>
		///<value>The kernel.</value>
		internal static IKernel Kernel { get { return _kernel ?? NinjectKernel.Instance; } }

		/// <summary>Sets the bindings.</summary>
		/// <param name="bind">The binding function.</param>
		public static void SetBindings(Action<IKernel> bind) {
			bind(Kernel);
		}

		/// <summary>Sets the bindings.</summary>
		/// <param name="func">The function.</param>
		/// <param name="kernel">The kernel.</param>
		/// <returns>The Ninject Kernel.</returns>
		public static IKernel SetBindings(Func<IKernel, IKernel> func, IKernel kernel) {
			_kernel = func(kernel);
			return _kernel;
		}

		///<summary>Gets a binding of type.</summary>
		///<typeparam name="TBinding">Type of the binding.</typeparam>
		///<param name="parameters">Options for controlling the operation.</param>
		///<returns>The binding of type&lt; t binding&gt;</returns>
		public static TBinding GetBindingOfType<TBinding>(params IParameter[] parameters) {
			return Kernel.TryGet<TBinding>(parameters);
		}

		///<summary>Gets a binding of type.</summary>
		///<param name="binding">The binding.</param>
		///<param name="parameters">Options for controlling the operation.</param>
		///<returns>The binding of type.</returns>
		public static Object GetBindingOfType(Type binding, params IParameter[] parameters) {
			return Kernel.TryGet(binding, parameters);
		}
	}
}