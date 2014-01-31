using System;
using System.Web.Mvc;
using Framework.Extensions;

namespace Framework.Web.Routing
{
	///<summary>Permanent redirect result.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public sealed class PermanentRedirectResult : ActionResult
	{
		private readonly string _url;

		///<summary>Constructor.</summary>
		///<remarks>Mhines, 11/29/2012.</remarks>
		///<exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
		///<param name="url">URL of the document.</param>
		public PermanentRedirectResult(string url) {
			if (!url.HasValue()) {
				throw new ArgumentNullException("url");
			}
			_url = url;
		}

		#region Overrides of ActionResult

		/// <summary>Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.</summary>
		/// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
		public override void ExecuteResult(ControllerContext context) {
			if (context.IsNull()) {
				throw new ArgumentNullException("context");
			}
			context.HttpContext.Response.RedirectPermanent(_url, true);
		}

		#endregion
	}
}