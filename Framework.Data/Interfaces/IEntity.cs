namespace Framework.Data.Interfaces
{
	/// <summary>Interface for entity.</summary>
	/// <typeparam name="TKey">Type of the key.</typeparam>
	public interface IEntity<TKey>
	{
		/// <summary>Gets or sets the identifier.</summary>
		/// <value>The identifier.</value>
		TKey Id { get; set; }
	}
}