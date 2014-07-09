// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" >
// </copyright>
// <summary>
//   Contract for UnitOfWork pattern.
//   For more information see http://martinfowler.com/eaaCatalog/unitOfWork.html
//   and http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
//   Unit Of Work is implemented in  ADO.NET Entity Framework
//   but this pattern is implemented to mantain Persistence Ignorance in the Business layer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Framework.Core.Interfaces;
using Framework.Data.Enumerations;
using Ninject.Parameters;

namespace Framework.Data.Interfaces
{
	/// <summary>Contract for UnitOfWork pattern.</summary>
	public interface IUnitOfWork : IDisposable
	{
		///<summary>Gets or sets the number of unit of works.</summary>
		///<value>The number of unit of works.</value>
		int UnitOfWorkCount { get; set; }

		///<summary>Gets or sets a stack of unit of works.</summary>
		///<value>A Stack of unit of works.</value>
		Queue<IUnitOfWork> UnitOfWorkQueue { get; set; }

		/// <summary>Gets or sets the Unit Of Work options that determine the behavior for Commits and Rollbacks.</summary>
		/// <value>The default options.</value>
		UnitOfWorkOptions DefaultOptions { get; set; }

		/// <summary>Gets or sets the current Status of this Unit Of Work.</summary>
		/// <value>The status.</value>
		UnitOfWorkStatus Status { get; set; }

		/// <summary>Gets or sets the Audit information to use during the SaveChanges().</summary>
		/// <value>Information describing the audit.</value>
		AuditInfo AuditInfo { get; set; }

		/// <summary>Gets a value indicating whether the Unit of Work is at the Top Level.</summary>
		/// <value>true if this object is top level, false if not.</value>
		bool IsTopLevel { get; }

		/// <summary>Gets a context for the curren.</summary>
		/// <value>The curren context.</value>
		IDataContext CurrenContext { get; }

		/// <summary>Gets the repository.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The repository&lt; t entity&gt;</returns>
		[Obsolete("This is no longer used, please use IDependencyParameter based method instead.", true)]
		IRepository<TEntity> GetRepository<TEntity>(params IParameter[] parameters) where TEntity : class, new();

		/// <summary>Gets the repository.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The repository&lt; t entity&gt;</returns>
		IRepository<TEntity> GetRepository<TEntity>(IDependencyParameter[] parameters) where TEntity : class, new(); 

		/// <summary>Commit all pending changes to repository.</summary>
		/// <returns>A value indicating the result of the commit operation.</returns>
		int Commit();

		/// <summary>Rollback changes not already commited.</summary>
		void RollbackChanges();
	}
}