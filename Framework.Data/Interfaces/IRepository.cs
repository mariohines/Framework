using System;
using System.Linq.Expressions;

namespace Framework.Data.Interfaces
{
	/// <summary>Interface for IRepository of TEntity.</summary>
	public interface IRepository<TEntity> : IDisposable 
		where TEntity : class, new()
	{
		/// <summary>Adds entities.</summary>
		/// <param name="entities">The entities to add.</param>
		void Add(params TEntity[] entities);

		/// <summary>Adds an entity.</summary>
		/// <param name="entity">The entity to add.</param>
		void Add(TEntity entity);

		/// <summary>Removes the given entities.</summary>
		/// <param name="parameters">The parameters to delete on.</param>
		void Remove(params Expression<Func<TEntity, bool>>[] parameters);

		/// <summary>Removes the given entities.</summary>
		/// <param name="entites">The entity to remove.</param>
		void Remove(params TEntity[] entites);

		/// <summary>Removes the given entities.</summary>
		/// <param name="entity">The entity to remove.</param>
		void Remove(TEntity entity);

		/// <summary>Updates the given entities.</summary>
		/// <param name="entities">The entities to add.</param>
		void Update(params TEntity[] entities);

		/// <summary>Attaches the given entities.</summary>
		/// <param name="entities">The entities to add.</param>
		void Attach(params TEntity[] entities);
	}
}