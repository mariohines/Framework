// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InMemoryObjectSet.cs" >
// </copyright>
// <summary>
//   In memory IObjectSet. This class is intended only
//   for testing purposes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Framework.Data.Collections
{
	/// <summary>In memory IObjectSet. This class is intended only for testing purposes.</summary>
	/// ### <typeparam name="TEntity">Type of elements in objectSet.</typeparam>
	public sealed class InMemoryObjectSet<TEntity> : IObjectSet<TEntity>
		where TEntity : class
	{
		#region Members

		/// <summary>Holds the values in this set.</summary>
		private readonly List<TEntity> _innerList;

		/// <summary>Holds the Inlcuded Paths.</summary>
		private readonly List<string> _includePaths;

		#endregion

		#region Constructor

		/// <summary>Initializes a new instance of the <see cref="InMemoryObjectSet{TEntity}"/> class.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="innerList">A List{T} with inner values of this IObjectSet.</param>
		public InMemoryObjectSet(List<TEntity> innerList) {
			if (innerList == null) {
				throw new ArgumentNullException("innerList");
			}

			_innerList = innerList;
			_includePaths = new List<string>();
		}

		#endregion

		#region IQueryable Properties

		/// <summary>
		/// Gets the type of the element(s) that are returned when the expression tree associated with this instance of System.Linq.IQueryable is
		/// executed.
		/// </summary>
		/// <value>The type of the element.</value>
		public Type ElementType {
			get { return typeof (TEntity); }
		}

		/// <summary>Gets the expression tree that is associated with the instance of System.Linq.IQueryable.</summary>
		/// <value>The expression.</value>
		public System.Linq.Expressions.Expression Expression {
			get { return _innerList.AsQueryable().Expression; }
		}

		/// <summary>Gets the query provider that is associated with this data source.</summary>
		/// <value>The provider.</value>
		public IQueryProvider Provider {
			get { return _innerList.AsQueryable().Provider; }
		}

		#endregion

		#region Methods

		/// <summary>Include path in query objects.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="path">Path to include.</param>
		/// <returns>IObjectSet with include path.</returns>
		public InMemoryObjectSet<TEntity> Include(string path) {
			if (String.IsNullOrEmpty(path)) {
				throw new ArgumentNullException("path");
			}

			_includePaths.Add(path);

			return this;
		}

		#endregion

		#region IObjectSet<T> Members

		/// <summary>Adds a new object to this set.</summary>
		/// <param name="entity">Object of type {TEntity} to add to this set.</param>
		public void AddObject(TEntity entity) {
			if (entity != null) {
				_innerList.Add(entity);
			}
		}

		/// <summary>Adds an existing object to this set.</summary>
		/// <param name="entity">Object of type {TEntity} to add to this set.</param>
		public void Attach(TEntity entity) {
			if (entity != null && !_innerList.Contains(entity)) {
				_innerList.Add(entity);
			}
		}

		/// <summary>Remove an object from the set.</summary>
		/// <param name="entity">Object of type {TEntity} to remove from this set.</param>
		public void Detach(TEntity entity) {
			if (entity != null) {
				_innerList.Remove(entity);
			}
		}

		/// <summary>Delete object from Set.</summary>
		/// <param name="entity">Object of type {TEntity} to delete from this set.</param>
		public void DeleteObject(TEntity entity) {
			if (entity != null) {
				_innerList.Remove(entity);
			}
		}

		#endregion

		#region IEnumerable<T> Members

		/// <summary>Returns an enumerator of type {TEntity} that interates through the inner collection.</summary>
		/// <returns>Enumerator of type {TEntity} that interates through the inner collection.</returns>
		public IEnumerator<TEntity> GetEnumerator() {
			return ((IEnumerable<TEntity>) _innerList).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <summary>Returns an enumerator that interates through the collection.</summary>
		/// <returns>Enumerator that interates through the collection.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}

		#endregion
	}
}