using System.Data.Entity;

namespace Framework.Data.Interfaces
{
	/// <summary>An IDataContext immplementation for DbContext.</summary>
	public interface IDbContext : IDataContext
	{
		/// <summary>Method to attach an entity to the context.</summary>
		/// <typeparam name="TEntity">An object of the specified type.</typeparam>
		/// <param name="entity">The entity to attach to the context.</param>
		/// <returns>The attached object.</returns>
		TEntity Attach<TEntity>(TEntity entity) where TEntity : class, new();

		/// <summary>Method to create an object collection of the specified type.</summary>
		/// <typeparam name="TEntity">A DbContext object.</typeparam>
		/// <returns>A IDbSet of the specified type.</returns>
		IDbSet<TEntity> CreateDbSet<TEntity>() where TEntity : class, new();

		/// <summary>Method to set the changes to a specified object.</summary>
		/// <typeparam name="TEntity">An object of the specified type.</typeparam>
		/// <param name="entity">The entity to make changes to.</param>
		void SetChanges<TEntity>(TEntity entity) where TEntity : class, new();
	}
}