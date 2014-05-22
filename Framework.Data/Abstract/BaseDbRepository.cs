using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Framework.Core.Extensions;
using Framework.Data.Enumerations;
using Framework.Data.Interfaces;
using LinqKit;

namespace Framework.Data.Abstract
{
	/// <summary></summary>
	/// <typeparam name="TEntity"></typeparam>
	public abstract class BaseDbRepository<TEntity> : IDbRepository<TEntity>
		where TEntity : class, new()
	{
		private bool _isDisposed;

		/// <summary>Gets the current object inherited from IDbContext.</summary>
		protected IDbContext Context { get; private set; }

		/// <summary>Gets the current IDbSet of type <typeparamref name="TEntity"/>.</summary>
		protected internal IDbSet<TEntity> ItemSet { get; private set; }

		#region Constructors

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <param name="context">Context for the database.</param>
		protected BaseDbRepository(IDbContext context) {
			Context = context;
			ItemSet = context.CreateDbSet<TEntity>();
		}

		/// <summary>Finaliser.</summary>
		~BaseDbRepository() {
			Dispose(false);
		}

		#endregion End Constructors

		#region Implementation of IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <param name="isDisposing">true if this object is disposing.</param>
		protected virtual void Dispose(bool isDisposing) {
			if (_isDisposed) return;
			if (isDisposing) {
				Context.Dispose();
			}
			_isDisposed = true;
		}

		#endregion

		#region Implementation of IDbRepository<T>

		/// <summary>Generic method to 'Find' a single object based on the primary keys.</summary>
		/// <param name="keys">The primary key(s) of the object.</param>
		/// <returns>A single object of type TEntity.</returns>
		[DebuggerNonUserCode]
		public virtual TEntity Find(params object[] keys) {
			return ItemSet.Find(keys);
		}

		/// <summary>Generic method to 'Get' a single object based on the passed in IQuery parameters.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A single object of type TEntity.</returns>
		[DebuggerNonUserCode]
		public virtual TEntity Get(params Expression<Func<TEntity, bool>>[] parameters) {
			return parameters.Aggregate(ItemSet.AsExpandable(), (current, expression) => current.Where(expression)).SingleOrDefault();
		}

		/// <summary>Generic method to 'Get' a single object based on the passed in IQuery parameters.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A single object of type TEntity asynchronously.</returns>
		[DebuggerNonUserCode]
		public virtual async Task<TEntity> GetAsync(params Expression<Func<TEntity, bool>>[] parameters) {
			return await Task<TEntity>.Factory.StartNew(() => Get(parameters));
		}

		/// <summary>Generic method to 'Get' a collection of T.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>A collection of type TEntity.</returns>
		[DebuggerNonUserCode]
		public virtual IEnumerable<TEntity> GetEntities(params Expression<Func<TEntity, bool>>[] parameters) {
			return parameters.Aggregate(ItemSet.AsExpandable(), (current, expression) => current.Where(expression)).ToList();
		}

		/// <summary>Generic method to 'Get' a collection of T.</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <param name="maxRows">The max number of rows to take.</param>
		/// <returns>A collection of type TEntity.</returns>
		[DebuggerNonUserCode]
		public virtual IEnumerable<TEntity> GetEntities(int maxRows, params Expression<Func<TEntity, bool>>[] parameters) {
			return parameters.Aggregate(ItemSet.AsExpandable(), (current, expression) => current.Where(expression))
				.Take(maxRows)
				.ToList();
		}

		/// <summary>Generic method for 'Any' across the collection of T..</summary>
		/// <param name="parameters">An array of expressions to query by.</param>
		/// <returns>true if it succeeds, false if it fails.</returns>
		[DebuggerNonUserCode]
		public virtual bool Any(params Expression<Func<TEntity, bool>>[] parameters) {
			return parameters.Aggregate(ItemSet.AsExpandable(), (current, expression) => current.Where(expression)).Any();
		}

		/// <summary>Gets entities count.</summary>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The entities count.</returns>
		[DebuggerNonUserCode]
		public virtual long GetEntitiesCount(params Expression<Func<TEntity, bool>>[] parameters) {
			return parameters.Aggregate(ItemSet.AsExpandable(), (current, expression) => current.Where(expression)).LongCount();
		}

		/// <summary>Updates this object.</summary>
		/// <param name="filterExpression">The filter expression.</param>
		/// <param name="updateExpression">The update expression.</param>
		[DebuggerNonUserCode]
		public virtual void Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression) {
			ItemSet.AsExpandable().Where(filterExpression).Update(updateExpression);
		}

		/// <summary>Adds entities.</summary>
		/// <param name="entities">The entities to add.</param>
		[DebuggerNonUserCode]
		public virtual void Add(params TEntity[] entities) {
			entities.ForEach(Add);
		}

		/// <summary>Generic method to add objects to the object context.</summary>
		/// <param name="entity"></param>
		[DebuggerNonUserCode]
		public virtual void Add(TEntity entity) {
			ItemSet.Add(entity);
		}

		/// <summary>Removes the given entities.</summary>
		/// <param name="expressions">The entities to add.</param>
		[DebuggerNonUserCode]
		public virtual void Remove(params Expression<Func<TEntity, bool>>[] expressions) {
			expressions.Aggregate(ItemSet.AsExpandable(), (current, expression) => current.Where(expression)).Delete();
		}

		/// <summary>Removes the given entities.</summary>
		/// <param name="entites">The entites to remove.</param>
		[Obsolete("This method is no longer valid.", true)]
		public virtual void Remove(params TEntity[] entites) {
			throw new NotImplementedException();
		}

		/// <summary>Removes the given entities.</summary>
		/// <param name="entity">The entity to remove.</param>
		[DebuggerNonUserCode]
		public virtual void Remove(TEntity entity) {
			ItemSet.Remove(entity);
		}

		/// <summary>Method to update the specified entities.</summary>
		/// <param name="entities">The entities to update.</param>
		[DebuggerNonUserCode]
		public virtual void Update(params TEntity[] entities) {
			entities.ForEach(e => Context.SetChanges(e));
		}

		/// <summary>Attaches the given entities.</summary>
		/// <param name="entities">The entities to add.</param>
		[DebuggerNonUserCode]
		public void Attach(params TEntity[] entities) {
			entities.ForEach(e => Context.Attach(e));
		}

		/// <summary>Method to save the changes to the ObjectContext.</summary>
		/// <param name="strategy">(optional) the strategy in case of a DbConcurrencyException.</param>
		/// <returns>An integer value from the DbContext.</returns>
		[Obsolete("This method is no longer valid.", true)]
		public int SaveChanges(ConcurrencyStrategy? strategy = null) {
			bool isSaved;
			var saveReturn = -1;
			do {
				isSaved = false;
				try {
					saveReturn = Context.SaveChanges();
					isSaved = true;
				}
				catch (DbUpdateConcurrencyException concurrencyException) {
					foreach (var entity in concurrencyException.Entries) {
						switch (strategy) {
							case ConcurrencyStrategy.Database:
								entity.Reload();
								break;
							default:
								entity.CurrentValues.SetValues(entity.GetDatabaseValues());
								break;
						}
					}
				}
			} while (isSaved);
			return saveReturn;
		}

		/// <summary>Method to execute a stored procedure.</summary>
		/// <param name="procedureName">The name of the procedure.</param>
		/// <param name="parameters">The parameter array.</param>
		[DebuggerNonUserCode]
		public virtual void ExecuteProcedure(string procedureName, Dictionary<string, object> parameters) {
			if (string.IsNullOrWhiteSpace(procedureName)) {
				throw new ArgumentNullException("procedureName");
			}
			var builder = new StringBuilder();
			builder.AppendFormat("{0} ", procedureName.Replace(" ", string.Empty));
			builder.Append(parameters.Keys.Join(","));
			Context.ExecuteProcedure(builder.ToString(), parameters.Values.ToArray());
		}

		#endregion
	}

	/// <summary>A base database repository.</summary>
	/// <typeparam name="TDbContext">Type of the database context.</typeparam>
	/// <typeparam name="TEntity">Type of the entity.</typeparam>
	public abstract class BaseDbRepository<TDbContext, TEntity> : BaseDbRepository<TEntity>
		where TDbContext : class, IDbContext
		where TEntity : class, new()
	{
		/// <summary>Gets the current object inherited from <typeparamref name="TDbContext"/>.</summary>
		protected new TDbContext Context { get; private set; }

		#region Constructors

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <param name="context">The current object inherited from <typeparamref name="TDbContext"/>.</param>
		protected BaseDbRepository(TDbContext context) : base(context) {
			Context = context;
		}

		/// <summary>Finaliser.</summary>
		~BaseDbRepository() {
			Dispose(false);
		}

		#endregion
	}
}