using System.Web.Mvc;
using Framework.Core.Collections;

namespace Framework.Web.Mvc.Abstract
{
	///<summary>Base mvc controller.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public abstract class BaseMvcController : Controller
	{
		private dynamic _tempBag;

		/// <summary>Gets temporary data dynmaically.</summary>
		public dynamic TempBag {
			get { return _tempBag ?? (_tempBag = new DynamicCollection(TempData)); }
		}
	}
}