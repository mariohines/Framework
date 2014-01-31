using System;

namespace Framework.Data.Interfaces
{
	/// <summary>Definition of an interface to process business rules.</summary>
	/// <typeparam name="TEntity">An object that is usually a POCO class.</typeparam>
	public interface IDbBusinessRule<TEntity> : IDisposable 
		where TEntity : class, new()
	{
		/// <summary>The next IDbBusinessRule of type <typeparamref name="TEntity"/> to process.</summary>
		IDbBusinessRule<TEntity> NextRule { get; }

		/// <summary>Method to process a rule.</summary>
		/// <param name="action">The passed in action to process.</param>
		/// <returns>An IDbBusiness rule of type <typeparamref name="TEntity"/>.</returns>
		IDbBusinessRule<TEntity> ProcessRule(Action action);

		/// <summary>Method to process a rule.</summary>
		/// <param name="action">The passed in action on type <typeparamref name="TEntity"/> to process.</param>
		/// <param name="entity">The entity to process.</param>
		/// <returns>An IDbBusiness rule of type <typeparamref name="TEntity"/>.</returns>
		IDbBusinessRule<TEntity> ProcessRule(Action<TEntity> action, TEntity entity);

		/// <summary>Method to process a rule.</summary>
		/// <param name="func">The passed in function to process.</param>
		/// <returns>An IDbBusiness rule of type <typeparamref name="TEntity"/>.</returns>
		IDbBusinessRule<TEntity> ProcessRule(Func<object> func);

		/// <summary>Method to process a rule.</summary>
		/// <param name="func">The passed in function of type <typeparamref name="TEntity"/> to process.</param>
		/// <param name="entity">The entity to process.</param>
		/// <returns>An IDbBusiness rule of type <typeparamref name="TEntity"/>.</returns>
		IDbBusinessRule<TEntity> ProcessRule(Func<TEntity, object> func, TEntity entity);

		/// <summary>Method to complete processing of rules. [Abstract in base classes]</summary>
		void CompleteProcessing();
	}
}