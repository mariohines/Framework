using System;
using System.Web.UI;
using Framework.Core.IoC;
using Framework.Core.IoC.Ninject;
using Framework.Web.Interfaces;

namespace Framework.Web.Abstract
{
	/// <summary>A base view.</summary>
	/// <typeparam name="TPresenter">Type of the presenter.</typeparam>
	public abstract class BaseView<TPresenter> : Page, IView<TPresenter>
		where TPresenter : IPresenter
	{
		#region Implementation of IView<out TPresenter>

		/// <summary>Gets the presenter.</summary>
		/// <value>The presenter.</value>
		public TPresenter Presenter { get; private set; }

		#endregion

		/// <summary>Specialised default constructor for use only by derived classes.</summary>
		protected BaseView() {
			Presenter = GenericIocManager.IsInUse
				? GenericIocManager.GetBindingOfType<TPresenter>()
				: NinjectManager.GetBindingOfType<TPresenter>();
		}

		#region Overrides of Control

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data. </param>
		protected override void OnLoad(EventArgs e) {
			base.OnLoad(e);
			Presenter.LoadView();
		}

		#endregion
	}
}