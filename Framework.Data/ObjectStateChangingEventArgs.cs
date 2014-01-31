using System;
using Framework.Data.Enumerations;

namespace Framework.Data
{
	/// <summary>Additional information for object state changing events.</summary>
	public class ObjectStateChangingEventArgs : EventArgs
	{
		/// <summary>Gets or sets the state of the new.</summary>
		/// <value>The new state.</value>
		public ObjectState NewState { get; set; }
	}
}