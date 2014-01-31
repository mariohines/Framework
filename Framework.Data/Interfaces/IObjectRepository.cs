// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" >
// </copyright>
// <summary>
//   Base interface for "Repository Pattern",
//   for more information about this pattern see http://martinfowler.com/eaaCatalog/repository.html
//   or http://blogs.msdn.com/adonet/archive/2009/06/16/using-repository-and-unit-of-work-patterns-with-entity-framework-4-0.aspx
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Data.Interfaces
{
	/// <summary>Base interface for "Repository Pattern"</summary>
	/// <typeparam name="TEntity">An Entity type.</typeparam>
	public interface IObjectRepository<TEntity> : IRepository<TEntity>
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		#region Methods

		/// <summary>Returns a single entitity based on specification query paramaters.</summary>
		/// <param name="specs">specification Parameters.</param>
		/// <returns>entity object.</returns>
		TEntity Get(params ISpecification<TEntity>[] specs);

		/// <summary>Returns a single entitity based on specification query paramaters asynchronously.</summary>
		/// <param name="specs">specification Parameters.</param>
		/// <returns>entity object asynchronously.</returns>
		Task<TEntity> GetAsync(params ISpecification<TEntity>[] specs);

		/// <summary>Get elements from repository that match the Filter, Sorting and Paging specifications.</summary>
		/// <param name="specs">specification Parameters.</param>
		/// <returns>Elements from the repository that match the filer specification and ordered by sort specification.</returns>
		IEnumerable<TEntity> GetElements(params ISpecification<TEntity>[] specs);

		/// <summary>Get elements from repository that match the Filter, Sorting and Paging specifications asynchronously.</summary>
		/// <param name="specs">specification Parameters.</param>
		/// <returns>Elements from the repository that match the filer specification and ordered by sort specification asynchronously.</returns>
		Task<IEnumerable<TEntity>> GetElementsAsync(params ISpecification<TEntity>[] specs);

		/// <summary>Get elements from repository that match the Filter, Sorting and Paging specifications.</summary>
		/// <returns>Elements from the repository that match the filer specification and ordered by sort specification.</returns>
		long GetElementCount(params ISpecification<TEntity>[] specs);

		#endregion
	}
}