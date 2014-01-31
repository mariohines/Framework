using System;
using System.Linq.Expressions;
using Framework.Data.Interfaces;

namespace Framework.Data.Specifications
{
	/// <summary>().</summary>
	public class IncludeSpecification<TEntity>
		: ISpecification<TEntity>
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		private readonly Expression<Func<TEntity, object>> _includeExpression;

		#region constructors

		/// <summary>Constructor.</summary>
		/// <param name="includeExpression">.</param>
		public IncludeSpecification(Expression<Func<TEntity, object>> includeExpression) {
			_includeExpression = includeExpression;
		}

		#endregion

		/// <summary>Gets the include expression.</summary>
		/// <value>The include expression.</value>
		public Expression<Func<TEntity, object>> IncludeExpression {
			get { return _includeExpression; }
		}
	}
}