using System;
using System.Diagnostics;
using Framework.Data.Interfaces;
using Framework.Extensions;

namespace Framework.Data.Abstract
{
	/// <summary>An abstract base class for implementation of IObjectBusinessRule.</summary>
	/// <typeparam name="TRepository">An object that inherits from IObjectRepository.</typeparam>
	/// <typeparam name="TEntity">An object that is usually a POCO class within an ObjectContext.</typeparam>
	public abstract class BaseObjectBusinessRule<TRepository, TEntity> : IObjectBusinessRule<TEntity> 
		where TEntity : class, IObjectWithChangeTracker, new()
		where TRepository : IObjectRepository<TEntity>
	{
		private bool _isDisposed;

		/// <summary>Gets the protected repository based on IObjectRepository.</summary>
		protected TRepository Repository { get; private set; }

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <param name="repository">The protected repository based on IObjectRepository.</param>
		protected BaseObjectBusinessRule (TRepository repository)
			: this(repository, null) {}

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <param name="repository">The protected repository based on IObjectRepository.</param>
		/// <param name="nextRule">The next IObjectBusinessRule of type <typeparamref name="TEntity"/> to process.</param>
		protected BaseObjectBusinessRule(TRepository repository, IObjectBusinessRule<TEntity> nextRule) {
			Repository = repository;
			NextRule = nextRule;
		}

		/// <summary>Finaliser.</summary>
		~BaseObjectBusinessRule() {
			Dispose(false);
		}

		#region Implementation of IObjectBusinessRule<TEntity>

		/// <summary>The next IObjectBusinessRule of type <typeparamref name="TEntity"/> to process.</summary>
		public IObjectBusinessRule<TEntity> NextRule { get; private set; }

		/// <summary>Method to process a rule.</summary>
		/// <param name="action">The passed in action to process.</param>
		/// <returns>An IObjectBusinessRule of type <typeparamref name="TEntity"/>.</returns>
		[DebuggerNonUserCode]
		public virtual IObjectBusinessRule<TEntity> ProcessRule(Action action) {
			if (!action.IsNull()) {
				action();
			}
			return NextRule.IsNull() ? this : NextRule;
		}

		/// <summary>Method to process a rule.</summary>
		/// <param name="action">The passed in action on type <typeparamref name="TEntity"/> to process.</param>
		/// <param name="entity">The entity to process.</param>
		/// <returns>An IObjectBusinessRule of type <typeparamref name="TEntity"/>.</returns>
		[DebuggerNonUserCode]
		public virtual IObjectBusinessRule<TEntity> ProcessRule(Action<TEntity> action, TEntity entity) {
			if (!action.IsNull()) {
				action(entity);
			}
			return NextRule.IsNull() ? this : NextRule;
		}

		/// <summary>Method to process a rule.</summary>
		/// <param name="func">The passed in function to process.</param>
		/// <returns>An IObjectBusinessRule of type <typeparamref name="TEntity"/>.</returns>
		[DebuggerNonUserCode]
		public virtual IObjectBusinessRule<TEntity> ProcessRule(Func<object> func) {
			var result = default(object);
			if (!func.IsNull()) {
				result = func();
			}
			return result is IObjectBusinessRule<TEntity>
			       	? result as IObjectBusinessRule<TEntity>
			       	: NextRule.IsNull()
			       	  	? this
			       	  	: NextRule;
		}

		/// <summary>Method to process a rule.</summary>
		/// <param name="func">The passed in function of type <typeparamref name="TEntity"/> to process.</param>
		/// <param name="entity">The entity to process.</param>
		/// <returns>An IObjectBusinessRule of type <typeparamref name="TEntity"/>.</returns>
		[DebuggerNonUserCode]
		public virtual IObjectBusinessRule<TEntity> ProcessRule(Func<TEntity, object> func, TEntity entity) {
			var result = default(object);
			if (!func.IsNull()) {
				result = func(entity);
			}
			return result is IObjectBusinessRule<TEntity>
							? result as IObjectBusinessRule<TEntity>
							: NextRule.IsNull()
									? this
									: NextRule;
		}

		/// <summary>Method to complete processing of rules. [Abstract in base classes]</summary>
		public abstract void CompleteProcessing();

		#endregion

		#region Implementation of IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		[DebuggerNonUserCode]
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <param name="isDisposing">true if this object is disposing.</param>
		protected virtual void Dispose(bool isDisposing)
		{
			if (_isDisposed) return;
			if (isDisposing)
			{
				NextRule = null;
				Repository.Dispose();
			}
			_isDisposed = true;
		}

		#endregion
	}
}