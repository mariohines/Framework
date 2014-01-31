// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidUnitOfWorkException.cs" >
// </copyright>
// <summary>
//   Represents errors related to an invalid UnitOfWork object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Data.Exceptions
{
	/// <summary>
	/// This exception is thrown when no UnitOfWork context exits and a Write operation was called in a repository or SaveChanges in the
	/// ObjectContext.
	/// </summary>
	public class InvalidUnitOfWorkException
		: UnitOfWorkException
	{
		/// <summary>Initializes a new instance of the <see cref="InvalidUnitOfWorkException"/> class.</summary>
		public InvalidUnitOfWorkException()
			: base("Invalid UnitOfWork or it does not exist.") {
		}
	}
}