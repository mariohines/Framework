using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Data.Interfaces
{
	/// <summary>Interface for raven repository.</summary>
	/// <typeparam name="TEntity">Type of the entity.</typeparam>
	public interface IRavenRepository<TEntity> : IDisposable
	{
		/// <summary>Generic method to 'Get' a single object based on the passed in IQuery parameters.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A single object of type T.</returns>
		TEntity Get (params Expression<Func<TEntity, bool>>[] parameters);

		/// <summary>Generic method to 'Get' a collection of T.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A collection of type T.</returns>
		IEnumerable<TEntity> GetEntities (params Expression<Func<TEntity, bool>>[] parameters);

		/// <summary>Generic method to 'Get' a collection of T.</summary>
		/// <param name="maxRows">Max number of rows to take.</param>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A collection of type T.</returns>
		IEnumerable<TEntity> GetEntities (int maxRows, params Expression<Func<TEntity, bool>>[] parameters);

		/// <summary></summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A long count.</returns>
		long GetEntitiesCount (params Expression<Func<TEntity, bool>>[] parameters);

		/// <summary>Method to add an entity to the DocumentStore.</summary>
		/// <param name="entity">The entity to add.</param>
		void Add (TEntity entity);

		/// <summary>Generic method to add objects to the object context.</summary>
		/// <param name="entities">An array of T objects.</param>
		void Add (params TEntity[] entities);

		/// <summary>Method to update the specified entity.</summary>
		/// <param name="entity">The entity to update.</param>
		void Update (TEntity entity);

		/// <summary>Method to update the specified entities.</summary>
		/// <param name="entities">The entities to update.</param>
		void Update (params TEntity[] entities);

		/// <summary>Method to save the changes to the DocumentStore.</summary>
		/// <returns>An integer value from the DocumentStore.</returns>
		int SaveChanges ();

		/// <summary>Method to attach an object to the DocumentStore.</summary>
		/// <param name="entity">The specified entity to attach.</param>
		/// <returns>The attached object of the specified type.</returns>
		TEntity Attach (TEntity entity);

		/// <summary>Method to remove an object from the DocumentStore.</summary>
		/// <param name="entity">The entity to remove.</param>
		void Remove (TEntity entity);

		/// <summary>Method to remove the specified objects from the DocumentStore.</summary>
		/// <param name="entities">The specified objects to remove.</param>
		void Remove (params TEntity[] entities);
	}
}
