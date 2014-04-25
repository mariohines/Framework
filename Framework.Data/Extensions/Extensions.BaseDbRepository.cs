using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Core.Extensions;
using Framework.Data.Abstract;
using Framework.Data.Interfaces;
using LinqKit;

namespace Framework.Data.Extensions
{
	public static partial class Extensions
	{
		/// <summary>A BaseDbRepository&lt;TEntity&gt; extension method that searches for the first match.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="repository">The repository to act on.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>An object of type <typeparamref name="TEntity"/>.</returns>
		public static TEntity Find<TEntity>(this IDbRepository<TEntity> repository, params Expression<Func<TEntity, bool>>[] parameters)
			where TEntity : class, new() {
			var repo = (BaseDbRepository<TEntity>) repository;
			var result =
				parameters.Aggregate(repo.ItemSet.Local.AsQueryable().AsExpandable(), (current, expression) => current.Where(expression))
						  .SingleOrDefault();
			return result ?? repository.Get(parameters);
		}

		/// <summary>A BaseDbRepository&lt;TEntity&gt; extension method that searches for the first asynchronous.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="repository">The repository to act on.</param>
		/// <param name="keys">A variable-length parameters list containing keys.</param>
		/// <returns>The found async&lt; t entity&gt;</returns>
		public static async Task<TEntity> FindAsync<TEntity>(this IDbRepository<TEntity> repository, params object[] keys)
			where TEntity : class, new() {
			return await Task<TEntity>.Factory.StartNew(() => repository.Find(keys));
		}

		/// <summary>A BaseDbRepository&lt;TEntity&gt; extension method that searches for the first asynchronous.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="repository">The repository to act on.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The found async&lt; t entity&gt;</returns>
		public static async Task<TEntity> FindAsync<TEntity>(this IDbRepository<TEntity> repository, params Expression<Func<TEntity, bool>>[] parameters)
			where TEntity : class, new() {
			return await Task<TEntity>.Factory.StartNew(() => repository.Find(parameters: parameters));
		}

		/// <summary>Finds entities in this collection.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="repository">The repository to act on.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>An enumerator that allows foreach to be used to find entities&lt; t entity&gt; in this collection.</returns>
		public static IEnumerable<TEntity> FindEntities<TEntity>(this IDbRepository<TEntity> repository, params Expression<Func<TEntity, bool>>[] parameters)
			where TEntity : class, new() {
			var repo = (BaseDbRepository<TEntity>) repository;
			var results =
				parameters.Aggregate(repo.ItemSet.Local.AsQueryable().AsExpandable(), (current, expression) => current.Where(expression));
			return results.IsEmptyOrNull() ? repository.GetEntities(parameters) : results;
		}
	}
}