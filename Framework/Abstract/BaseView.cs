using System;
using Framework.Core.Builders;
using Framework.Core.Interfaces;

namespace Framework.Core.Abstract
{
	/// <summary>The abstract class to inherit a page from.</summary>
	public abstract class BaseView<TPresenter> : IView<TPresenter>
		where TPresenter : class, IPresenter
	{
		/// <summary>Loads a delegate.</summary>
		/// <param name="sender">Source of the event.</param>
		/// <param name="args">Event information.</param>
		protected delegate void LoadDelegate(object sender, EventArgs args);

		/// <summary>Event queue for all listeners interested in load events.</summary>
		protected event LoadDelegate LoadEvent;

		/// <summary>Specialised default constructor for use only by derived classes.</summary>
		protected BaseView() {
			Presenter = PresenterFactory<TPresenter>.CreatePresenter(this);
		}

		/// <summary>Executes the load action.</summary>
		protected virtual void OnLoad() {
			var handler = LoadEvent;
			if (handler == null) return;
			handler(this, EventArgs.Empty);
			Presenter.LoadView();
		}

		#region Implementation of IView<out TPresenter>

		/// <summary>Gets the presenter.</summary>
		/// <value>The presenter.</value>
		public TPresenter Presenter { get; private set; }

		#endregion
	}
}