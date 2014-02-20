﻿using System.Web.Mvc;
using Framework.Collections;

namespace Framework.Web.Mvc.Abstract
{
	///<summary>Base asynchronous mvc controller.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public abstract class BaseAsyncMvcController : AsyncController
	{
		private dynamic _tempBag;

		/// <summary>Gets temporary data dynmaically.</summary>
		public dynamic TempBag {
			get { return _tempBag ?? (_tempBag = new DynamicCollection(TempData)); }
		}
	}
}