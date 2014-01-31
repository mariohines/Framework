using System;

namespace Framework.Interfaces
{
	/// <summary>Interface for notify complex property changing.</summary>
	public interface INotifyComplexPropertyChanging
	{
		/// <summary>Event queue for all listeners interested in ComplexPropertyChanging events.</summary>
		event EventHandler ComplexPropertyChanging;
	}
}