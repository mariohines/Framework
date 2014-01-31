using System;
using System.Collections.Concurrent;

namespace Framework.Data.Interfaces
{
	/// <summary>Interface for data context.</summary>
	public interface IDataContext : IDisposable
	{
		/// <summary>Gets the Unit of Work Counter.</summary>
		/// <value>The number of unit of works.</value>
		int UnitOfWorkCount { get; }

		/// <summary>Gets or sets the Repository Counter.</summary>
		/// <value>The number of repositories.</value>
		int RepositoryCount { get; set; }

		/// <summary>Gets the UnitOfWork stack.</summary>
		/// <value>A Stack of unit of works.</value>
		ConcurrentStack<IUnitOfWork> UnitOfWorkStack { get; }

		/// <summary>Method to execute a stored procedure.</summary>
		/// <param name="commandText">The text of the command to run.</param>
		/// <param name="parameters">The parameter array.</param>
		void ExecuteProcedure(string commandText, params object[] parameters);

		/// <summary>Method to save changes.</summary>
		/// <returns>An integer value from the database.</returns>
		int SaveChanges();
	}
}