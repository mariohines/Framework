using System;
using Framework.Data.Interfaces;

namespace Framework.Data.Abstract
{
	/// <summary>().</summary>
	public abstract class BaseObjectRepositoryEx<TObjectContext, TEntity> : BaseObjectRepository<TEntity> 
		where TObjectContext : class, IObjectContext
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		/// <summary>Gets the current object inherited from <typeparamref name="TObjectContext"/>.</summary>
		protected new TObjectContext Context { get; private set; }

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="context">The current object inherited from <typeparamref name="TObjectContext"/>.</param>
		protected BaseObjectRepositoryEx(TObjectContext context) : base(context) {
			Context = context;
		}

		/// <summary>Finaliser.</summary>
		~BaseObjectRepositoryEx() {
			Dispose(false);
		}
	}
}