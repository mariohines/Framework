namespace Framework.Data.Interfaces
{
	/// <summary>Interface for object with change tracker.</summary>
	public interface IObjectWithChangeTracker
	{
		/// <summary>Gets the change tracker.</summary>
		/// <value>Has all the change tracking information for the subgraph of a given object.</value>
		ObjectChangeTracker ChangeTracker { get; }
	}
}