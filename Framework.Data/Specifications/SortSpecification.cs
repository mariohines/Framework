using System;
using System.Linq.Expressions;
using Framework.Data.Enumerations;
using Framework.Data.Interfaces;

namespace Framework.Data.Specifications
{
	/// <summary>().</summary>
	public class SortSpecification<TEntity>
		: ISpecification<TEntity>
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		private readonly Expression<Func<TEntity, object>> _sortByExpression;
		private readonly string _sortColumnName;
		private readonly SortDirection _sortDirection;

		#region constructors

		/// <summary>Constructor.</summary>
		/// <param name="columnName">Name of the column.</param>
		/// <param name="sortDirection">(optional) the sort direction.</param>
		public SortSpecification(string columnName, SortDirection sortDirection = SortDirection.Ascending) {
			_sortByExpression = null;
			_sortColumnName = columnName;
			_sortDirection = sortDirection;
		}

		/// <summary>Constructor.</summary>
		/// <param name="columnExpression">The column expression.</param>
		/// <param name="sortDirection">(optional) the sort direction.</param>
		public SortSpecification(Expression<Func<TEntity, object>> columnExpression,
		                         SortDirection sortDirection = SortDirection.Ascending) {
			_sortColumnName = null;
			_sortByExpression = columnExpression;
			_sortDirection = sortDirection;
		}

		#endregion

		/// <summary>Gets the sort by expression.</summary>
		/// <value>The sort by expression.</value>
		public Expression<Func<TEntity, object>> SortByExpression {
			get { return _sortByExpression; }
		}

		/// <summary>Gets the name of the sort column.</summary>
		/// <value>The name of the sort column.</value>
		public string SortColumnName {
			get { return _sortColumnName; }
		}

		/// <summary>Gets the sort direction.</summary>
		/// <value>The sort direction.</value>
		public SortDirection SortDirection {
			get { return _sortDirection; }
		}
	}
}