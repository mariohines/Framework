using System;

namespace Framework.Interfaces
{
	/// <summary>Interface for dependency injector.</summary>
	/// <remarks>The purpose of this interface is to abstract away the dependency injection framework choice.</remarks>
	public interface IDependencyInjector
	{
		/// <summary>Gets the container.</summary>
		/// <value>The container.</value>
		object Container { get; }

		/// <summary>Binds this object.</summary>
		/// <typeparam name="TSource">Type of the source.</typeparam>
		/// <typeparam name="TDestination">Type of the destination.</typeparam>
		void Bind<TSource, TDestination>() where TDestination : TSource;

		/// <summary>Binds.</summary>
		/// <param name="source">Source for the.</param>
		/// <param name="destination">Destination for the.</param>
		void Bind(Type source, Type destination);

		/// <summary>Bind self.</summary>
		/// <typeparam name="TSource">Type of the source.</typeparam>
		void BindSelf<TSource>();

		/// <summary>Bind self.</summary>
		/// <param name="source">Source for the.</param>
		void BindSelf(Type source);

		/// <summary>Gets a binding.</summary>
		/// <typeparam name="TBinding">Type of the binding.</typeparam>
		/// <returns>.</returns>
		TBinding GetBinding<TBinding>();

		/// <summary>Gets a binding.</summary>
		/// <param name="binding">The binding.</param>
		/// <returns>The binding.</returns>
		object GetBinding(Type binding);
	}

	/// <summary>Interface for dependency injector.</summary>
	/// <typeparam name="TSource">Type of the source.</typeparam>
	public interface IDependencyInjector<out TSource> : IDependencyInjector
	{
		/// <summary>Gets the container.</summary>
		/// <value>The container.</value>
		new TSource Container { get; }
	}
}