using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Data.Interfaces
{
	/// <summary>Interface that implements the repository pattern.</summary>
	/// <typeparam name="TEntity">An object of the specified type.</typeparam>
	public interface IDbRepository<TEntity> : IRepository<TEntity>
		where TEntity : class, new()
	{
		/// <summary>Generic method to 'Find' a single object based on the primary keys.</summary>
		/// <param name="keys">The primary key(s) of the object.</param>
		/// <returns>A single object of type TEntity.</returns>
		TEntity Find(params object[] keys);

		/// <summary>Generic method to 'Get' a single object based on the passed in IQuery parameters.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A single object of type T.</returns>
		TEntity Get(params Expression<Func<TEntity, bool>>[] parameters);

		/// <summary>Generic method to 'Get' a single object based on the passed in IQuery parameters.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A single object of type T asynchronously.</returns>
		Task<TEntity> GetAsync(params Expression<Func<TEntity, bool>>[] parameters);

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

		/// <summary>Method to execute a stored procedure.</summary>
		/// <param name="procedureName">The name of the procedure.</param>
		/// <param name="parameters">The parameter array.</param>
		void ExecuteProcedure(string procedureName, Dictionary<string, object> parameters);
	}
}
