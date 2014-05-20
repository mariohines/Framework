// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWork.cs" >
// </copyright>
// <summary>
//   Defines the UnitOfWork type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Transactions;
using Framework.Core.IoC;
using Framework.Core.IoC.Ninject;
using Framework.Data.Enumerations;
using Framework.Data.Exceptions;
using Framework.Data.Interfaces;
using Ninject.Parameters;

namespace Framework.Data.Abstract
{
	/// <summary>
	/// Defines the UnitOfWork type.
	/// </summary>
	public sealed class UnitOfWork : IUnitOfWork
	{
		/// <summary>Gets the Unit of Work context.</summary>
		private readonly IDataContext _dataContext;

		/// <summary>Gets the TransactionScope of this Unit of Work.</summary>
		private readonly TransactionScope _transactionScope;

		/// <summary>Initializes a new instance of the <see cref="UnitOfWork"/> class.</summary>
		/// <param name="dataContext">Data context to use in UoW.</param>
		public UnitOfWork(IDataContext dataContext) {
			// set Default behavior.
			DefaultOptions = UnitOfWorkOptions.AutoCommit;
			Status = UnitOfWorkStatus.Active;

			_dataContext = dataContext;

			// Push this UoW into the stack.
			_dataContext.UnitOfWorkStack.Push(this);

			// create Transaction scope..
			_transactionScope = new TransactionScope();
		}

		/// <summary>Finalizes an instance of the <see cref="UnitOfWork"/> class.</summary>
		~UnitOfWork() {
			Dispose(false);
		}

		/// <summary>Gets a value indicating whether the Unit of Work is at the Top Level.</summary>
		/// <value>true if this object is top level, false if not.</value>
		public bool IsTopLevel {
			get { return _dataContext.UnitOfWorkCount == 1; }
		}

		/// <summary>Gets or sets the number of unit of works.</summary>
		/// <value>The number of unit of works.</value>
		public int UnitOfWorkCount { get; set; }

		/// <summary>Gets or sets a stack of unit of works.</summary>
		/// <value>A Stack of unit of works.</value>
		public Queue<IUnitOfWork> UnitOfWorkQueue { get; set; }

		/// <summary>Gets or sets the Default behavior for Commits and Rollbacks for this Unit Of Work.</summary>
		/// <value>The default options.</value>
		public UnitOfWorkOptions DefaultOptions { get; set; }

		/// <summary>Gets or sets the Status of this Unit Of Work.</summary>
		/// <value>The status.</value>
		public UnitOfWorkStatus Status { get; set; }

		/// <summary>Gets or sets the Audit information to use during the SaveChanges().</summary>
		/// <value>Information describing the audit.</value>
		public AuditInfo AuditInfo { get; set; }

		/// <summary>Return a new instance of the UnitOfWork class with the default behavior.</summary>
		/// <returns>A new instance of the UnitOfWork class.</returns>
		public static IUnitOfWork GetUnitOfWork() {
			return GetUnitOfWork(UnitOfWorkOptions.AutoCommit);
		}

		/// <summary>Return a new instance of the UnitOfWork class with the default behavior.</summary>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <returns>A new instance of the UnitOfWork class.</returns>
		public static IUnitOfWork GetUnitOfWork<TDataContext>() where TDataContext : IDataContext {
			return GetUnitOfWork<TDataContext>(UnitOfWorkOptions.AutoCommit);
		}

		/// <summary>
		/// Return the current instance of the Unit of Work class or create a new instance of the UnitOfWork class with the default behavior.
		/// </summary>
		/// <exception cref="InvalidUnitOfWorkException">Thrown when an invalid unit of work error condition occurs.</exception>
		/// <param name="createNew">A value indicating whether to create a New UoW or return the existing UoW.</param>
		/// <returns>An instance of the UnitOfWork class.</returns>
		public static IUnitOfWork GetUnitOfWork(bool createNew) {
			if (createNew) {
				return GetUnitOfWork(UnitOfWorkOptions.AutoCommit);
			}

			// Return existing UoW if there is one..
			// Get instance to ObjectContext..
			var context = GenericIocManager.IsInUse
				? GenericIocManager.GetBindingOfType<IUnitOfWork>()
				: NinjectManager.GetBindingOfType<IUnitOfWork>();
			if (context.UnitOfWorkCount == 0) {
				throw new InvalidUnitOfWorkException();
			}

			// Get current UnitOfWork.
			return context.UnitOfWorkQueue.Peek();
		}

		/// <summary>Return a new instance of the UnitOfWork class with the default behavior.</summary>
		/// <exception cref="InvalidUnitOfWorkException">Thrown when an Invalid Unit Of Work error condition occurs.</exception>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <param name="createNew">A value indicating whether to create a New UoW or return the existing UoW.</param>
		/// <returns>A new instance of the UnitOfWork class.</returns>
		public static IUnitOfWork GetUnitOfWork<TDataContext>(bool createNew)
			where TDataContext : IDataContext {
			if (createNew) {
				return GetUnitOfWork<TDataContext>(UnitOfWorkOptions.AutoCommit);
			}

			// Return existing UoW if there is one..
			// Get instance to ObjectContext..
			var context = GenericIocManager.IsInUse
				? GenericIocManager.GetBindingOfType<IUnitOfWork>()
				: NinjectManager.GetBindingOfType<IUnitOfWork>();
			if (context.UnitOfWorkCount == 0) {
				throw new InvalidUnitOfWorkException();
			}

			// Get current UnitOfWork.
			return context.UnitOfWorkQueue.Peek();
		}

		/// <summary>Return a new instance of the Unit of Work class with the specified behavior.</summary>
		/// <param name="defaultOption">The default Behavior for the new unitOfWork object.</param>
		/// <returns>A new instance of the UnitOfWork class with the Default behavior set.</returns>
		public static IUnitOfWork GetUnitOfWork(UnitOfWorkOptions defaultOption) {
			var newUoW = GenericIocManager.IsInUse
				? GenericIocManager.GetBindingOfType<IUnitOfWork>()
				: NinjectManager.GetBindingOfType<IUnitOfWork>();
			newUoW.DefaultOptions = defaultOption;
			return newUoW;
		}

		/// <summary>Return a new instance of the UnitOfWork class with the default behavior.</summary>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <param name="defaultOption">The default Behavior for the new unitOfWork object.</param>
		/// <returns>A new instance of the UnitOfWork class.</returns>
		public static IUnitOfWork GetUnitOfWork<TDataContext>(UnitOfWorkOptions defaultOption)
			where TDataContext : IDataContext {
			var context = GenericIocManager.IsInUse
				? GenericIocManager.GetBindingOfType<TDataContext>()
				: NinjectManager.GetBindingOfType<TDataContext>();
			return new UnitOfWork(context) {DefaultOptions = defaultOption};
		}

		/// <summary>Execute unit of work code block inside a unit of work.</summary>
		/// <param name="codeBlock">code block to execute inside as a unit of work.</param>
		public static void Do(Action<IUnitOfWork> codeBlock) {
			Do(UnitOfWorkOptions.AutoCommit, codeBlock);
		}

		/// <summary>Execute unit of work code block inside a unit of work.</summary>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <param name="codeBlock">code block to execute inside as a unit of work.</param>
		public static void Do<TDataContext>(Action<IUnitOfWork> codeBlock) where TDataContext : IDataContext {
			Do<TDataContext>(UnitOfWorkOptions.AutoCommit, codeBlock);
		}

		/// <summary>Execute unit of work code block inside a unit of work.</summary>
		/// <param name="defaultOption">The default Behavior for the new unitOfWork object.</param>
		/// <param name="codeBlock">code block to execute inside as a unit of work.</param>
		public static void Do(UnitOfWorkOptions defaultOption, Action<IUnitOfWork> codeBlock) {
			Do(defaultOption, null, codeBlock);
		}

		/// <summary>Execute unit of work code block inside a unit of work.</summary>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <param name="defaultOption">The default Behavior for the new unitOfWork object.</param>
		/// <param name="codeBlock">code block to execute inside as a unit of work.</param>
		public static void Do<TDataContext>(UnitOfWorkOptions defaultOption, Action<IUnitOfWork> codeBlock)
			where TDataContext : IDataContext {
			Do<TDataContext>(defaultOption, null, codeBlock);
		}

		/// <summary>Execute code block inside a unit of work.</summary>
		/// <param name="defaultOption">The default Behavior for the new unitOfWork object.</param>
		/// <param name="auditInfo">AuditInfo class to use during save operation.</param>
		/// <param name="codeBlock">code block to execute inside as a unit of work.</param>
		public static void Do(UnitOfWorkOptions defaultOption, AuditInfo auditInfo, Action<IUnitOfWork> codeBlock) {
			var unitOfWork = OnEntryAdvice(defaultOption);
			unitOfWork.AuditInfo = auditInfo;
			try {
				codeBlock(unitOfWork);
				OnSuccessAdvice(unitOfWork);
			}
			catch {
				OnExceptionAdvice(unitOfWork);
				throw;
			}
			finally {
				OnExitAdvice(unitOfWork);
			}
		}

		/// <summary>Execute unit of work code block inside a unit of work.</summary>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <param name="defaultOption">The default Behavior for the new unitOfWork object.</param>
		/// <param name="auditInfo">AuditInfo class to use during save operation.</param>
		/// <param name="codeBlock">code block to execute inside as a unit of work.</param>
		public static void Do<TDataContext>(UnitOfWorkOptions defaultOption, AuditInfo auditInfo, Action<IUnitOfWork> codeBlock)
			where TDataContext : IDataContext {
			var unitOfWork = OnEntryAdvice<TDataContext>(defaultOption);
			unitOfWork.AuditInfo = auditInfo;
			try {
				codeBlock(unitOfWork);
				OnSuccessAdvice(unitOfWork);
			}
			catch {
				OnExceptionAdvice(unitOfWork);
				throw;
			}
			finally {
				OnExitAdvice(unitOfWork);
			}
		}

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Gets the repository.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <returns>The repository&lt; t entity&gt;</returns>
		public IRepository<TEntity> GetRepository<TEntity>(params IParameter[] parameters) where TEntity : class, new() {
			var parameterList = new List<IParameter>(parameters) {new ConstructorArgument("context", _dataContext)};
			return GenericIocManager.IsInUse
				? GenericIocManager.GetBindingOfType<IRepository<TEntity>>()
				: NinjectManager.GetBindingOfType<IRepository<TEntity>>(parameterList.ToArray());
		}

		/// <summary>Commit all changes to repository.</summary>
		/// <returns>A value indicating the result of the commit operation.</returns>
		public int Commit() {
			var result = 0;

			if (IsTopLevel) {
				// save changes.
				result = _dataContext.SaveChanges();
			}

			_transactionScope.Complete();

			Status = UnitOfWorkStatus.Committed;
			return result;
		}

		/// <summary>Rollback changes not already commited.</summary>
		public void RollbackChanges() {
			// Rollback changes.
			Status = UnitOfWorkStatus.RolledBack;
			Transaction.Current.Rollback();
		}

		#region Internal Methods for UnitOfWorkAttribute

		/// <summary>
		/// This is an Internal method used by UnitOfWork postsharp Aspect. Create a new instance of the UnitOfWork class with the specified
		/// behavior.
		/// </summary>
		/// <param name="defaultOption">behaviour for new UnitOfWork.</param>
		/// <returns>Instance of new Unit Of Work.</returns>
		internal static IUnitOfWork OnEntryAdvice(UnitOfWorkOptions defaultOption) {
			return GetUnitOfWork(defaultOption);
		}

		/// <summary>
		/// This is an Internal method used by UnitOfWork postsharp Aspect. Create a new instance of the UnitOfWork class with the specified
		/// behavior.
		/// </summary>
		/// <typeparam name="TDataContext">Type of the data context.</typeparam>
		/// <param name="defaultOption">behaviour for new UnitOfWork.</param>
		/// <returns>Instance of new Unit Of Work.</returns>
		internal static IUnitOfWork OnEntryAdvice<TDataContext>(UnitOfWorkOptions defaultOption)
			where TDataContext : IDataContext {
			return GetUnitOfWork<TDataContext>(defaultOption);
		}

		/// <summary>This is an Internal method used by the UnitOfWork postsharp Aspect. Perform Auto-Commit logic.</summary>
		/// <param name="unitOfWork">Instance to UnitOfWork.</param>
		internal static void OnSuccessAdvice(IUnitOfWork unitOfWork) {
			if (unitOfWork != null && unitOfWork.Status == UnitOfWorkStatus.Active &&
			    unitOfWork.DefaultOptions.HasFlag(UnitOfWorkOptions.AutoCommit)) {
				// Commit.
				unitOfWork.Commit();
			}
		}

		/// <summary>This is an Internal method used by the UnitOfWork postsharp Aspect. Dispose of UnitOfWork class.</summary>
		/// <param name="unitOfWork">Instance to UnitOfWork.</param>
		internal static void OnExitAdvice(IUnitOfWork unitOfWork) {
			if (unitOfWork != null) {
				unitOfWork.Dispose();
			}
		}

		/// <summary>This is an Internal method used by the UnitOfWork postsharp Aspect. Rollback UnitOfWork.</summary>
		/// <param name="unitOfWork">Instance to UnitOfWork.</param>
		internal static void OnExceptionAdvice(IUnitOfWork unitOfWork) {
			if (unitOfWork != null && unitOfWork.Status == UnitOfWorkStatus.Active) {
				// then Roll back.
				unitOfWork.RollbackChanges();
			}
		}

		#endregion

		/// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
		/// <param name="disposing">Value indicating whether object is being disposed.</param>
		public void Dispose(bool disposing) {
			if (Status != UnitOfWorkStatus.Disposed) {
				if (disposing) {
					// Dispose transaction scope.
					_transactionScope.Dispose();

					IUnitOfWork unitOfWork;
					// Decrease UnitOfWork counter.
					_dataContext.UnitOfWorkStack.TryPop(out unitOfWork);
				}
			}

			Status = UnitOfWorkStatus.Disposed;
		}
	}
}