// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IObjectContext.cs" >
// </copyright>
// <summary>
//   Defines the IObjectContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity.Core.Objects;

namespace Framework.Data.Interfaces
{
	/// <summary>This is the basic contract for a Context.</summary>
	public interface IObjectContext : IDataContext
	{
		/// <summary>Apply changes made in item or related items in the object graph.</summary>
		/// <typeparam name="TEntity">Type of item.</typeparam>
		/// <param name="item">Item with changes.</param>
		void SetChanges<TEntity>(TEntity item) where TEntity : class, IObjectWithChangeTracker, new();

		/// <summary>Attach object to container.</summary>
		/// <typeparam name="TEntity">Type of object to attach in container.</typeparam>
		/// <param name="item">Item to attach in container.</param>
		void Attach<TEntity>(TEntity item) where TEntity : class, IObjectWithChangeTracker, new();

		/// <summary>Create a object set for a type TEntity.</summary>
		/// <typeparam name="TEntity">Type of elements in object set.</typeparam>
		/// <returns>Object set of type {TEntity}</returns>
		IObjectSet<TEntity> CreateObjectSet<TEntity>() where TEntity : class, IObjectWithChangeTracker, new();
	}
}