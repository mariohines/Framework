// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitOfWorkException.cs" >
// </copyright>
// <summary>
//   Represents errors related to the UnitOfWork object.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.Data.Exceptions
{
    /// <summary>Represents errors related to the UnitOfWork object.</summary>
	public class UnitOfWorkException
		: Exception
	{
        /// <summary>Initializes a new instance of the <see cref="UnitOfWorkException"/> class.</summary>
        /// <param name="message">The message related to this unitOfWork exception.</param>
		public UnitOfWorkException(string message)
			: base(message) {
		}
	}
}