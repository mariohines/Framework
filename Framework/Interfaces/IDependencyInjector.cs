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
		/// <param name="parameters">Options for controlling the operation.</param>
		void Bind<TSource, TDestination>(params IDependencyParameter[] parameters) where TDestination : TSource;

		/// <summary>Binds.</summary>
		/// <param name="source">Source for the.</param>
		/// <param name="destination">Destination for the.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		void Bind(Type source, Type destination, params IDependencyParameter[] parameters);

		/// <summary>Bind self.</summary>
		/// <typeparam name="TSource">Type of the source.</typeparam>
		/// <param name="parameters">Options for controlling the operation.</param>
		void BindSelf<TSource>(params IDependencyParameter[] parameters);

		/// <summary>Bind self.</summary>
		/// <param name="source">Source for the.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		void BindSelf(Type source, params IDependencyParameter[] parameters);

		/// <summary>Gets a binding.</summary>
		/// <typeparam name="TBinding">Type of the binding.</typeparam>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The binding&lt; t binding&gt;</returns>
		TBinding GetBinding<TBinding>(params IDependencyParameter[] parameters);

		/// <summary>Gets a binding.</summary>
		/// <param name="binding">The binding.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The binding.</returns>
		object GetBinding(Type binding, params IDependencyParameter[] parameters);
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