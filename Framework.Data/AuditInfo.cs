// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuditInfo.cs" >
// </copyright>
// <summary>
//   Defines the AuditInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.Data
{
	/// <summary>
	/// Defines the AuditInfo type
	/// </summary>
	public class AuditInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuditInfo"/> class. 
		/// </summary>
		public AuditInfo() {
			UserName = String.Empty;
			ActivityId = System.Diagnostics.Trace.CorrelationManager.ActivityId;
			Description = String.Empty;
		}

		/// <summary>
		/// Gets or sets the UserName performing the audited activity.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the activity Id.
		/// </summary>
		public Guid ActivityId { get; set; }

		/// <summary>
		/// Gets or sets a description for the audited activity.
		/// </summary>
		public string Description { get; set; }
	}
}