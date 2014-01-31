using System.Web.Mvc;

namespace Framework.Web.Mvc.Abstract
{
	/// <summary>Base form action.</summary>
	/// <typeparam name="TModel">.</typeparam>
	public abstract class BaseFormAction<TModel> : ActionResult
	{
		#region Public Members

		/// <summary>Gets or sets the model.</summary>
		/// <value>The model.</value>
		public TModel Model { get; private set; }

		/// <summary>Gets or sets the failure.</summary>
		/// <value>The failure.</value>
		public ViewResult Failure { get; private set; }

		/// <summary>Gets or sets the success.</summary>
		/// <value>The success.</value>
		public ActionResult Success { get; private set; } 
		#endregion End Public Members

		/// <summary>Specialised constructor for use only by derived classes.</summary>
		/// <param name="model">The model.</param>
		/// <param name="success">The success.</param>
		/// <param name="failure">The failure.</param>
		protected BaseFormAction (TModel model, ActionResult success, ViewResult failure) {
			Model = model;
			Success = success;
			Failure = failure;
		}
	} 
}