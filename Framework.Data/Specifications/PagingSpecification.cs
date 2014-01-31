using System;
using Framework.Data.Interfaces;

namespace Framework.Data.Specifications
{
	/// <summary>().</summary>
	public class PagingSpecification<TEntity>
		: ISpecification<TEntity>
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		private readonly int _pageIndex;
		private readonly int _pageSize;

		#region constructors

		/// <summary>Constructor.</summary>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when one or more arguments are outside the required range.</exception>
		/// <param name="pageIndex">(optional) zero-based index of the page.</param>
		/// <param name="pageSize">(optional) size of the page.</param>
		public PagingSpecification(int pageIndex = 0, int pageSize = 10) {
			if (pageIndex < 0) {
				throw new ArgumentOutOfRangeException("pageIndex", pageIndex, @"PageIndex cannot be less than zero.");
			}

			if (pageSize <= 0) {
				throw new ArgumentOutOfRangeException("pageSize", pageSize, @"PageSize must be greater than zero.");
			}

			_pageIndex = pageIndex;
			_pageSize = pageSize;
		}

		#endregion

		/// <summary>Gets zero-based index of the page.</summary>
		/// <value>The page index.</value>
		public int PageIndex {
			get { return _pageIndex; }
		}

		/// <summary>Gets the size of the page.</summary>
		/// <value>The size of the page.</value>
		public int PageSize {
			get { return _pageSize; }
		}
	}
}