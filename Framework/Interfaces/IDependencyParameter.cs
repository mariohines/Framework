namespace Framework.Core.Interfaces
{
	/// <summary>Interface for dependency parameter.</summary>
	public interface IDependencyParameter
	{
		/// <summary>Gets the source for the.</summary>
		/// <value>The source.</value>
		object Source { get; } 
	}

	/// <summary>Interface for dependency parameter.</summary>
	/// <typeparam name="TSource">Type of the source.</typeparam>
	public interface IDependencyParameter<out TSource> : IDependencyParameter
	{
		/// <summary>Gets the source for the.</summary>
		/// <value>The source.</value>
		new TSource Source { get; }
	}
}