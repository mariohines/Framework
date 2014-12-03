using System;
using System.Collections.Generic;
using Framework.Core.Extensions;
using Framework.Core.Interfaces;

namespace Framework.Core.IoC
{
	/// <summary>Manager for generic inversion of control.</summary>
	/// <remarks>Wrapper for managing other injection frameworks.</remarks>
	public class GenericIocManager
	{
		/// <summary>Gets or sets the injector.</summary>
		/// <value>The injector.</value>
		internal static IDependencyInjector Injector { get; private set; }

		/// <summary>Gets a value indicating whether this object is in use.</summary>
		/// <value>true if this object is in use, false if not.</value>
		public static bool IsInUse { get { return !Injector.IsNull(); } }

		/// <summary>Sets the bindings.</summary>
		/// <param name="bind">The bind.</param>
		/// <param name="injector">The injector.</param>
		/// <remarks>This call MUST be made before attemtping to get bindings.</remarks>
		public static void SetBindings(Func<IDependencyInjector, IDependencyInjector> bind, IDependencyInjector injector) {
			Injector = bind(injector);
		}

		/// <summary>Gets the binding of type.</summary>
		/// <typeparam name="TBinding">Type of the binding.</typeparam>
		/// <param name="parameters">The parameters that may be necessary to retrieve the binding.</param>
		/// <returns>The binding of type&lt; t binding&gt;</returns>
		public static TBinding GetBindingOfType<TBinding>(params IDependencyParameter[] parameters) {
			if (Injector.IsNull()) {
				throw new InvalidOperationException("The method 'SetBindings' has not been called to initialize the dependencies.");
			}
			return Injector.GetBinding<TBinding>(parameters);
		}

		/// <summary>Gets the bindings of types in this collection.</summary>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <typeparam name="TBinding">Type of the binding.</typeparam>
		/// <param name="parameters">The parameters that may be necessary to retrieve the binding.</param>
		/// <returns>An enumerator that allows foreach to be used to process the bindings of types in this collection.</returns>
		public static IEnumerable<TBinding> GetBindingsOfType<TBinding>(params IDependencyParameter[] parameters) {
			if (Injector.IsNull()) {
				throw new InvalidOperationException("The method 'SetBindings' has not been called to initialize the dependencies.");
			}
			return Injector.GetBindings<TBinding>(parameters);
		}

		/// <summary>Gets a binding of type.</summary>
		/// <param name="binding">The binding.</param>
		/// <param name="parameters">The parameters that may be necessary to retrieve the binding.</param>
		/// <returns>The binding of type.</returns>
		public static object GetBindingOfType(Type binding, params IDependencyParameter[] parameters) {
			if (Injector.IsNull()) {
				throw new InvalidOperationException("The method 'SetBindings' has not been called to initialize the dependencies.");
			}
			return Injector.GetBinding(binding, parameters);
		}

		/// <summary>Gets the bindings of types in this collection.</summary>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <param name="binding">The binding.</param>
		/// <param name="parameters">The parameters that may be necessary to retrieve the binding.</param>
		/// <returns>An enumerator that allows foreach to be used to process the bindings of types in this collection.</returns>
		public static IEnumerable<object> GetBindingsOfType(Type binding, params IDependencyParameter[] parameters) {
			if (Injector.IsNull()) {
				throw new InvalidOperationException("The method 'SetBindings' has not been called to initialize the dependencies.");
			}
			return Injector.GetBindings(binding, parameters);
		}
	}
}