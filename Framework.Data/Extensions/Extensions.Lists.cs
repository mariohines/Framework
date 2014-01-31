using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using Framework.Data.Collections;
using Framework.Data.Enumerations;
using Framework.Data.Interfaces;
using Framework.Data.Specifications;

namespace Framework.Data.Extensions
{
	public static partial class Extensions
	{
		#region Extension methods for List<TEntity>

		/// <summary>Convert a list of {TEntity} to an InMemoreyObjectSet of {TEntity}</summary>
		/// <typeparam name="TEntity">Type of TEntity.</typeparam>
		/// <param name="list">The original list.</param>
		/// <returns>Returns an InMemoryOjectSet of TEntity with items from this list.</returns>
		public static InMemoryObjectSet<TEntity> ToInMemoryObjectSet<TEntity> (this List<TEntity> list) where TEntity : class {
			return new InMemoryObjectSet<TEntity>(list);
		}

		#endregion

		#region Extension Methods for IQueryable<TSource>

		/// <summary>Extension method to allow for dynamic loading of Related Entities via the Include Method of the Entity Framework.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <typeparam name="TEntity">The type of the element or object.</typeparam>
		/// <param name="source">Initial ObjectQuery.</param>
		/// <param name="includeSpecification">Related Entities to include as part of the ObjectQuery.</param>
		/// <returns>A System.Linq.IQueryable{T} and requested related entities.</returns>
		public static IQueryable<TEntity> Include<TEntity>(this IQueryable<TEntity> source, IncludeSpecification<TEntity> includeSpecification)
			where TEntity : class, IObjectWithChangeTracker, new()
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			var objectQuery = source as ObjectQuery<TEntity>;
			return objectQuery == null
					   ? source
					   : objectQuery.Include(FuncToString(includeSpecification.IncludeExpression.Body as MemberExpression));
		}

		/// <summary>Page Results.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <typeparam name="TEntity">.</typeparam>
		/// <param name="source">.</param>
		/// <param name="filterSpecification">.</param>
		/// <returns>.</returns>
		public static IQueryable<TEntity> Filter<TEntity> (this IQueryable<TEntity> source, FilterSpecification<TEntity> filterSpecification)
			where TEntity : class, IObjectWithChangeTracker, new() {
			if (source == null) {
				throw new ArgumentNullException("source");
			}

			if (filterSpecification == null) {
				throw new ArgumentNullException("filterSpecification");
			}

			return source.Where(filterSpecification.Expression());
		}

		/// <summary>
		/// Sorts the elements of a sequence in either ascending or descending order, according to the specified SortSpecification.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <typeparam name="TEntity">.</typeparam>
		/// <param name="source">.</param>
		/// <param name="sortSpecification">.</param>
		/// <returns>.</returns>
		public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, SortSpecification<TEntity> sortSpecification)
			where TEntity : class, IObjectWithChangeTracker, new()
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (sortSpecification == null)
			{
				throw new ArgumentNullException("sortSpecification");
			}

			// Do we have a SortBy Expression?
			if (sortSpecification.SortByExpression != null)
			{
				return sortSpecification.SortDirection == SortDirection.Ascending
						   ? source.OrderBy(sortSpecification.SortByExpression)
						   : source.OrderByDescending(sortSpecification.SortByExpression);
			}

			// Or a Sort By Column Name?
			var type = typeof (TEntity);
			var property = type.GetProperty(sortSpecification.SortColumnName);
			if (property == null)
			{
				throw new InvalidOperationException(String.Format("Could not find a property called '{0}' on type {1}",
																  sortSpecification.SortColumnName, type));
			}

			var expression = Expression.Parameter(type, "p");
			var expression3 = Expression.Lambda(Expression.MakeMemberAccess(expression, property), new[] {expression});
			var methodName = (sortSpecification.SortDirection == SortDirection.Ascending) ? "OrderBy" : "OrderByDescending";
			var expression4 = Expression.Call(typeof (Queryable), methodName, new[] {type, property.PropertyType},
											  new[] {source.Expression, Expression.Quote(expression3)});
			return (IOrderedQueryable<TEntity>) source.Provider.CreateQuery<TEntity>(expression4);
		}

		#endregion

		#region Extension Methods for IOrderedQueryable<TSource>

		/// <summary>
		/// Sorts the elements of a sequence in either ascending or descending order, according to the specified SortSpecification.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <exception cref="InvalidOperationException">Thrown when the requested operation is invalid.</exception>
		/// <typeparam name="TEntity">.</typeparam>
		/// <param name="source">.</param>
		/// <param name="sortSpecification">.</param>
		/// <returns>.</returns>
		public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> source, SortSpecification<TEntity> sortSpecification)
			where TEntity : class, IObjectWithChangeTracker, new()
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (sortSpecification == null)
			{
				throw new ArgumentNullException("sortSpecification");
			}

			// Do we have a SortBy Expression?
			if (sortSpecification.SortByExpression != null)
			{
				return sortSpecification.SortDirection == SortDirection.Ascending
						   ? source.ThenBy(sortSpecification.SortByExpression)
						   : source.ThenByDescending(sortSpecification.SortByExpression);
			}

			// Or a Sort By Column Name?
			var type = typeof (TEntity);
			var property = type.GetProperty(sortSpecification.SortColumnName);
			if (property == null)
			{
				throw new InvalidOperationException(String.Format("Could not find a property called '{0}' on type {1}",
																  sortSpecification.SortColumnName, type));
			}

			var expression = Expression.Parameter(type, "p");
			var expression3 = Expression.Lambda(Expression.MakeMemberAccess(expression, property), new[] {expression});
			var methodName = (sortSpecification.SortDirection == SortDirection.Ascending) ? "ThenBy" : "ThenByDescending";
			var expression4 = Expression.Call(typeof (Queryable), methodName, new[] {type, property.PropertyType},
											  new[] {source.Expression, Expression.Quote(expression3)});
			return (IOrderedQueryable<TEntity>) source.Provider.CreateQuery<TEntity>(expression4);
		}

		/// <summary>Page Results.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when one or more arguments are outside the required range.</exception>
		/// <typeparam name="TEntity">.</typeparam>
		/// <param name="source">.</param>
		/// <param name="pagingSpecification">.</param>
		/// <returns>.</returns>
		public static IOrderedQueryable<TEntity> Paged<TEntity>(this IOrderedQueryable<TEntity> source, PagingSpecification<TEntity> pagingSpecification)
			where TEntity : class, IObjectWithChangeTracker, new()
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}

			if (pagingSpecification == null)
			{
				throw new ArgumentNullException("pagingSpecification");
			}

			if (pagingSpecification.PageIndex < 0)
			{
				throw new ArgumentOutOfRangeException("pagingSpecification", pagingSpecification.PageIndex,
													  @"PageIndex cannot be less than zero.");
			}

			if (pagingSpecification.PageSize <= 0)
			{
				throw new ArgumentOutOfRangeException("pagingSpecification", pagingSpecification.PageSize,
													  @"PageSize must be greater than zero.");
			}

			return
				(IOrderedQueryable<TEntity>)
				source.Skip(pagingSpecification.PageIndex*pagingSpecification.PageSize).Take(pagingSpecification.PageSize);
		}

		#endregion

		#region Helper method for "Include" functionality

		/// <summary>
		/// Determines the string as needed to properly format the EF "Include" syntax based on the requested entities to Include in the Query.
		/// </summary>
		/// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
		/// <param name="memberExpression">Linq Expression defining the related entities to return.</param>
		/// <returns>A string as needed to properly format the Query to include the requested Related Entities.</returns>
		private static string FuncToString(MemberExpression memberExpression)
		{
			if (memberExpression.Expression.NodeType == ExpressionType.Parameter)
			{
				return memberExpression.Member.Name;
			}

			if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
			{
				return String.Format("{0}.{1}", FuncToString(memberExpression.Expression as MemberExpression),
									 memberExpression.Member.Name);
			}

			var methodCallExpression = (MethodCallExpression) memberExpression.Expression;
			if (methodCallExpression.Arguments.Count != 1)
			{
				throw new Exception("invalid method call in Include expression");
			}

			return String.Format("{0}.{1}", FuncToString(methodCallExpression.Arguments[0] as MemberExpression),
								 memberExpression.Member.Name);
		}

		#endregion
	}
}
