using System;
using System.Web.UI;
using Framework.Web.Mvp.Interface;

namespace Framework.Web.Mvp.Abstract
{
	/// <summary>The abstract class to inherit a page from.</summary>
	public abstract class BaseView<TPresenter> : Page
		where TPresenter : class, IPresenter
	{
		/// <summary>Gets or sets the presenter.</summary>
		/// <value>The presenter.</value>
		protected TPresenter Presenter { get; private set; }

		/// <summary>Specialised default constructor for use only by derived classes.</summary>
		protected BaseView() {
			Presenter = PresenterFactory<TPresenter>.CreatePresenter(this);
		}

		#region Overrides of Control

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load"/> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			Presenter.LoadView();
		}

		#endregion
	}
}