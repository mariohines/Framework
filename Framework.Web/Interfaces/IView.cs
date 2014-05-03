namespace Framework.Web.Interfaces
{
	/// <summary>Interface for view.</summary>
	public interface IView
	{
		/// <summary>Gets the presenter.</summary>
		/// <value>The presenter.</value>
		object Presenter { get; }
	}

	/// <summary>Interface for view.</summary>
	/// <typeparam name="TPresenter">Type of the presenter.</typeparam>
	public interface IView<out TPresenter> where TPresenter : IPresenter
	{
		/// <summary>Gets the presenter.</summary>
		/// <value>The presenter.</value>
		TPresenter Presenter { get; }
	}
}