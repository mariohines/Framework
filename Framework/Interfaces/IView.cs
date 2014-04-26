namespace Framework.Core.Interfaces
{
	/// <summary>Interface for view.</summary>
	public interface IView<out TPresenter> where TPresenter : IPresenter
	{
		/// <summary>Gets the presenter.</summary>
		/// <value>The presenter.</value>
		TPresenter Presenter { get; }
	}
}