// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseRepository.cs" >
// </copyright>
// <summary>
//   Defines the BaseRepository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity.Core.Objects;
// ReSharper disable LoopCanBeConvertedToQuery
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Framework.Data.Enumerations;
using Framework.Data.Exceptions;
using Framework.Data.Extensions;
using Framework.Data.Interfaces;
using Framework.Data.Specifications;
using LinqKit;

namespace Framework.Data.Abstract
{
	/// <summary>
	/// This class implements the base Repository of TEntity.
	/// </summary>
	/// <typeparam name="TEntity">Type of Entity to use for this Repository.</typeparam>
	//[Trace(AttributeTargetElements = MulticastTargets.Method, AttributeTargetMemberAttributes = MulticastAttributes.Public)]
	public abstract class BaseObjectRepository<TEntity> : IObjectRepository<TEntity>
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		#region Private Fields.

		/// <summary>
		/// Internal Context used by this repository to persist changes.
		/// Context is normally supplied by IoC.
		/// </summary>
		public IObjectContext Context { get; private set; }

		/// <summary>
		/// Object Set.
		/// </summary>
		public virtual IObjectSet<TEntity> ItemSet { get; private set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseObjectRepository{TEntity}"/> class. 
		/// </summary>
		/// <param name="context">The context for this repository. Normally supplied by IoC. </param>
		protected BaseObjectRepository(IObjectContext context) {
			// check context.
			if (context == null) {
				throw new ArgumentNullException("context", "The context cannot be null");
			}

			// set internal values.
			Context = context;
			Context.RepositoryCount++;
			ItemSet = context.CreateObjectSet<TEntity>();
		}

		/// <summary>
		/// Finalizes an instance of the <see cref="BaseObjectRepository{TEntity}"/> class. 
		/// </summary>
		~BaseObjectRepository() {
			Dispose(false);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the Status of this Unit Of Work.
		/// </summary>
		private RepositoryStatus Status { get; set; }

		#endregion

		#region Methods

		/// <summary>Adds entities.</summary>
		/// <param name="entities">The entities to add.</param>
		[DebuggerNonUserCode]
		public void Add(params TEntity[] entities) {
			entities.ForEach(Add);
		}

		/// <summary>
		/// Add item to repository.
		/// </summary>
		/// <param name="item">Item to add to repository.</param>
		[DebuggerNonUserCode]
		public virtual void Add(TEntity item) {
			// check arguments
			if (item == null) {
				throw new ArgumentNullException("item", "'item' argument is null.");
			}

			// check Unit of work.
			if (Context.UnitOfWorkCount <= 0) {
				throw new InvalidUnitOfWorkException();
			}

			// add object to IObjectSet for this type
			if (ItemSet is ObjectSet<TEntity>) {
				//((ObjectSet<TEntity>)ItemSet).ApplyChanges(item);  
			}
			else {
				ItemSet.AddObject(item);
			}
		}

		/// <summary>
		/// Delete an item from repository.
		/// </summary>
		/// <param name="entity">Item to delete.</param>
		[DebuggerNonUserCode]
		public virtual void Remove(TEntity entity) {
			// check item
			if (entity == null) {
				throw new ArgumentNullException("entity", "'item' argument is null.");
			}

			// check Unit of work.
			if (Context.UnitOfWorkCount <= 0) {
				throw new InvalidUnitOfWorkException();
			}

			// Attach object to context and delete this
			// this is valid only if T is a type in model
			Context.Attach(entity);

			// delete object to IObjectSet for this type
			ItemSet.DeleteObject(entity);
		}

		/// <summary>
		/// Attach entity to repository.
		/// Attach is similar to add but the internal state
		/// for this object is not  marked as 'Added, Modifed or Deleted', submit changes
		/// in Unit Of Work don't send anything to storage.
		/// </summary>
		/// <param name="item">Item to attach.</param>
		[DebuggerNonUserCode]
		public virtual void Attach(TEntity item) {
			if (item == null) {
				throw new ArgumentNullException("item");
			}

			Context.Attach(item);
		}

		/// <summary>
		/// Sets modified entity into the repository. 
		/// When calling Commit() method in UnitOfWork 
		/// these changes will be saved into the storage.
		/// <remarks>
		/// Internally this method always calls Repository.Attach() and Context.SetChanges() 
		/// </remarks>
		/// </summary>
		/// <param name="item">Item with changes.</param>
		[DebuggerNonUserCode]
		public virtual void Modify(TEntity item) {
			// check arguments
			if (item == null) {
				throw new ArgumentNullException("item", "'item' argument is null.");
			}

			// check Unit of work.
			if (Context.UnitOfWorkCount <= 0) {
				throw new InvalidUnitOfWorkException();
			}

			// add object to IObjectSet for this type
			if (ItemSet is ObjectSet<TEntity>) {
				//((ObjectSet<TEntity>)ItemSet).ApplyChanges(item);
			}
			else {
				Context.SetChanges(item);
			}
		}

		/// <summary>
		/// Sets modified entities into the repository. 
		/// When calling Commit() method in UnitOfWork 
		/// these changes will be saved into the storage.
		/// </summary>
		/// <param name="items">Collection of items with changes.</param>
		[DebuggerNonUserCode]
		public virtual void Modify(ICollection<TEntity> items) {
			// check arguments
			if (items == null) {
				throw new ArgumentNullException("items", "'items' argument is null.");
			}

			// check Unit of work.
			if (Context.UnitOfWorkCount <= 0) {
				throw new InvalidUnitOfWorkException();
			}

			// for each element in collection apply changes
			foreach (var item in items.Where(item => item != null)) {
				Modify(item);
			}
		}

		/// <summary>
		/// Returns a single entitity based on specification query paramaters
		/// </summary>
		/// <remarks>only includes Filter and include Specficiation Types </remarks>
		/// <param name="specs">specification Parameters</param>
		/// <returns>entity object</returns>
		[DebuggerNonUserCode]
		public virtual TEntity Get(params ISpecification<TEntity>[] specs) {
			if (specs == null) {
				throw new ArgumentNullException("specs");
			}

			var query = ItemSet.AsQueryable();

			// Process Include specifications..);
			foreach (var specification in specs.OfType<IncludeSpecification<TEntity>>()) {
				query = query.Include(specification);
			}

			query = query.AsExpandable();

			// Process Filter specifications..
			foreach (var specification in specs.OfType<FilterSpecification<TEntity>>()) {
				query = query.Filter(specification);
			}

			return query.FirstOrDefault();
		}

		/// <summary>Returns a single entitity based on specification query paramaters asynchronously.</summary>
		/// <param name="specs">specification Parameters.</param>
		/// <returns>entity object asynchronously.</returns>
		[DebuggerNonUserCode]
		public async Task<TEntity> GetAsync(params ISpecification<TEntity>[] specs) {
			return await Task<TEntity>.Factory.StartNew(() => Get(specs));
		}

		/// <summary>
		/// Get elements from repository that match the Filter, Sorting and Paging specifications provided.
		/// </summary>
		/// <param name="specs">The specification on how to filter, include, page and sort items in the repository. </param>
		/// <returns>List of items that match the filter specifications, ordered and paged.</returns>
		[DebuggerNonUserCode]
		public virtual IEnumerable<TEntity> GetElements(params ISpecification<TEntity>[] specs) {
			if (specs == null) {
				throw new ArgumentNullException("specs");
			}

			var query = ItemSet.AsQueryable();

			// Process Include specifications..);
			foreach (var specification in specs.OfType<IncludeSpecification<TEntity>>()) {
				query = query.Include(specification);
			}

			query = query.AsExpandable();

			// Process Filter specifications..
			foreach (var specification in specs.OfType<FilterSpecification<TEntity>>()) {
				query = query.Filter(specification);
			}

			// Process Sort specifications..
			var orderBySet = false;
			foreach (var specification in specs.OfType<SortSpecification<TEntity>>()) {
				query = orderBySet == false
					? query.OrderBy(specification)
					: ((IOrderedQueryable<TEntity>) query).ThenBy(specification);
				orderBySet = true;
			}

			// Process Paging specification.
			var pagingSet = false;
			foreach (var specification in specs.OfType<PagingSpecification<TEntity>>()) {
				if (!orderBySet) {
					// cannot set pagging without Order By.
					throw new Exception("Cannot process PagingSpecification without a SortSpecification.");
				}

				if (pagingSet) {
					// cannot re-set pagging.
					throw new Exception("Only one PagingSpecification is allowed.");
				}

				// Process Sort specifications..
				query = ((IOrderedQueryable<TEntity>) query).Paged(specification);
				pagingSet = true;
			}

			// Execute query.
			return query.AsEnumerable();
		}

		/// <summary>Get elements from repository that match the Filter, Sorting and Paging specifications asynchronously.</summary>
		/// <param name="specs">specification Parameters.</param>
		/// <returns>Elements from the repository that match the filer specification and ordered by sort specification asynchronously.</returns>
		[DebuggerNonUserCode]
		public async Task<IEnumerable<TEntity>> GetElementsAsync(params ISpecification<TEntity>[] specs) {
			return await Task<IEnumerable<TEntity>>.Factory.StartNew(() => GetElements(specs));
		}

		/// <summary>
		/// Get elements from repository that match the Filter, Sorting and Paging specifications provided.
		/// </summary>
		/// <param name="specs">The filter specifications to use to select items from repository.</param>
		/// <returns>Count of items that match the filter specifications..</returns>
		[DebuggerNonUserCode]
		public virtual long GetElementCount(params ISpecification<TEntity>[] specs) {
			if (specs == null) {
				throw new ArgumentNullException("specs");
			}

			var query = ItemSet.AsExpandable();

			// Process Filter specifications..
			foreach (var specification in specs.OfType<FilterSpecification<TEntity>>()) {
				query = query.Filter(specification);
			}

			// Execute query.
			return query.LongCount();
		}

		#endregion

		#region IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">Value indicating whether object is being disposed.</param>
		protected virtual void Dispose(bool disposing) {
			if (Status != RepositoryStatus.Disposed) {
				if (disposing) {
					if (Status == RepositoryStatus.Active) {}

					// Decrease repository counter.
					Context.RepositoryCount--;
				}
			}

			Status = RepositoryStatus.Disposed;
		}

		#endregion

		#region Implementation of IRepository<in TEntity>

		/// <summary>Removes the given entities.</summary>
		/// <param name="parameters">The parameters to delete on.</param>
		[Obsolete("This is specific to DbContext. Do not use.", true)]
		public void Remove(params Expression<Func<TEntity, bool>>[] parameters) {
			throw new NotImplementedException();
		}

		/// <summary>Removes the given entities.</summary>
		/// <param name="entities">The entities to add.</param>
		public void Remove(params TEntity[] entities) {
			entities.ForEach(e => ItemSet.DeleteObject(e));
		}

		/// <summary>Method to update the specified entities.</summary>
		/// <param name="entities">The entities to update.</param>
		[DebuggerNonUserCode]
		public virtual void Update(params TEntity[] entities) {
			entities.ForEach(e => Context.SetChanges(e));
		}

		/// <summary>Attaches the given entities.</summary>
		/// <param name="entities">The entities to add.</param>
		public void Attach(params TEntity[] entities) {
			entities.ForEach(e => Context.Attach(e));
		}

		#endregion
	}

	/// <summary>().</summary>
	public abstract class BaseObjectRepository<TObjectContext, TEntity> : BaseObjectRepository<TEntity>
		where TObjectContext : class, IObjectContext
		where TEntity : class, IObjectWithChangeTracker, new()
	{
		/// <summary>Gets the current object inherited from <typeparamref name="TObjectContext"/>.</summary>
		protected new TObjectContext Context { get; private set; }

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		/// <param name="context">The current object inherited from <typeparamref name="TObjectContext"/>.</param>
		protected BaseObjectRepository(TObjectContext context) : base(context) {
			Context = context;
		}

		/// <summary>Finaliser.</summary>
		~BaseObjectRepository() {
			Dispose(false);
		}
	}
}