namespace Framework.Interfaces
{
	/// <summary>Interface for dependency parameter.</summary>
	public interface IDependencyParameter
	{
		object Source { get; } 
	}

	/// <summary>Interface for dependency parameter.</summary>
	/// <typeparam name="TSource">Type of the source.</typeparam>
	public interface IDependencyParameter<out TSource> : IDependencyParameter
	{
		new TSource Source { get; }
	}
}