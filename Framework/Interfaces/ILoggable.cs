using System;

namespace Framework.Interfaces
{
	/// <summary>Interface for loggable.</summary>
	public interface ILoggable
	{
		/// <summary>Logs an information.</summary>
		/// <param name="information">The information.</param>
		void LogInformation(string information);

		/// <summary>Logs a warning.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="warning">The warning.</param>
		/// <param name="exception">(optional) details of the exception.</param>
		/// <param name="warningData">(optional) information describing the warning.</param>
		void LogWarning<TEntity>(string warning, Exception exception = null, TEntity warningData = null) where TEntity : class;

		/// <summary>Logs an exception.</summary>
		/// <typeparam name="TEntity">Type of the entity.</typeparam>
		/// <param name="exception">(optional) details of the exception.</param>
		/// <param name="exceptionData">(optional) information describing the exception.</param>
		void LogException<TEntity>(Exception exception, TEntity exceptionData = null) where TEntity : class;
	}
}