using Framework.Data.Interfaces;

namespace Framework.Data.Abstract
{
	/// <summary></summary>
	/// <typeparam name="TDbContext"></typeparam>
	/// <typeparam name="TEntity"></typeparam>
	public abstract class BaseDbRepositoryEx<TDbContext, TEntity> : BaseDbRepository<TEntity> 
		where TDbContext : class, IDbContext
		where TEntity : class, new()
	{
		/// <summary>Gets the current object inherited from <typeparamref name="TDbContext"/>.</summary>
		protected new TDbContext Context { get; private set; }

		#region Constructors

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <param name="context">The current object inherited from <typeparamref name="TDbContext"/>.</param>
		protected BaseDbRepositoryEx(TDbContext context) : base(context) {
			Context = context;
		}

		/// <summary>Finaliser.</summary>
		~BaseDbRepositoryEx() {
			Dispose(false);
		}

		#endregion
	}
}