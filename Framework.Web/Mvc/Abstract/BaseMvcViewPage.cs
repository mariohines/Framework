using System.Web.Mvc;
using Framework.Core.Collections;

namespace Framework.Web.Mvc.Abstract
{
	///<summary>Base mvc view page.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public abstract class BaseMvcViewPage : WebViewPage
	{
		private dynamic _tempBag;

		/// <summary>Holds temporary data dynmaically.</summary>
		public dynamic TempBag {
			get { return _tempBag ?? (_tempBag = new DynamicCollection(TempData)); }
		}
	}

	///<summary>Base mvc view page.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	///<typeparam name="TModel">Type of the model.</typeparam>
	public abstract class BaseMvcViewPage<TModel> : BaseMvcViewPage
	{
		private ViewDataDictionary<TModel> _viewData;
		
		///<summary>
		///Gets or sets the <see cref="T:System.Web.Mvc.AjaxHelper" /> object that is used to render HTML using Ajax.
		///</summary>
		///<value>The <see cref="T:System.Web.Mvc.AjaxHelper" /> object that is used to render HTML using Ajax.</value>
		public new AjaxHelper<TModel> Ajax { get; set; }
		
		///<summary>
		///Gets or sets the <see cref="T:System.Web.Mvc.HtmlHelper" /> object that is used to render HTML elements.
		///</summary>
		///<value>The <see cref="T:System.Web.Mvc.HtmlHelper" /> object that is used to render HTML elements.</value>
		public new HtmlHelper<TModel> Html { get; set; }

		///<summary>
		///Gets the Model property of the associated <see cref="T:System.Web.Mvc.ViewDataDictionary" /> object.
		///</summary>
		///<value>The Model property of the associated <see cref="T:System.Web.Mvc.ViewDataDictionary" /> object.</value>
		public new TModel Model {
			get { return ViewData.Model; }
		}

		///<summary>Gets or sets a dictionary that contains data to pass between the controller and the view.</summary>
		///<value>A dictionary that contains data to pass between the controller and the view.</value>
		public new ViewDataDictionary<TModel> ViewData {
			get { return _viewData ?? (_viewData = new ViewDataDictionary<TModel>()); }
			set { SetViewData(value); }
		}

		///<summary>Initialises the helpers.</summary>
		///<remarks>Mhines, 11/4/2012.</remarks>
		public override void InitHelpers () {
			base.InitHelpers();
			Ajax = new AjaxHelper<TModel>(ViewContext, this);
			Html = new HtmlHelper<TModel>(ViewContext, this);
		}

		///<summary>Sets a view data.</summary>
		///<remarks>Mhines, 11/4/2012.</remarks>
		///<param name="viewData">Information describing the view.</param>
		protected override void SetViewData (ViewDataDictionary viewData) {
			_viewData = new ViewDataDictionary<TModel>(viewData);
			base.SetViewData(viewData);
		}
	}
}