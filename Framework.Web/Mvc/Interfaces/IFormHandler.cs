namespace Framework.Web.Mvc.Interfaces
{
	/// <summary>Interface to further abstract complex posting of objects.</summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IFormHandler<in TModel>
	{
		/// <summary>Method to process the model.</summary>
		/// <param name="model">The model to process.</param>
		void Handle(TModel model);
	}
}