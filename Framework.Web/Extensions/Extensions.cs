using System.Web.Mvc;
using Framework.Extensions;

namespace Framework.Web.Extensions
{
	///<summary>Extensions.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public static partial class Extensions
	{
		/// <summary>Extension method to change a string to an MvcHtmlString.</summary>
		/// <param name="s">The source string.</param>
		/// <returns>A MvcHtmlString value.</returns>
		public static MvcHtmlString ToMvcString (this string s) {
			return !s.HasValue() ? null : new MvcHtmlString(s);
		}
	}
}
