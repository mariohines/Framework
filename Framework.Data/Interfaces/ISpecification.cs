namespace Framework.Data.Interfaces
{
	/// <summary>Interface for entity.</summary>
	public interface ISpecification<TEntity> where TEntity : class, IObjectWithChangeTracker, new()
	{
	}
}