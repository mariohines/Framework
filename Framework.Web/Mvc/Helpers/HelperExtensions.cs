using System.Web.Mvc;
using Framework.Web.Builders;

namespace Framework.Web.Mvc.Helpers
{
	///<summary>Helper extensions.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public static class HelperExtensions
	{
		///<summary>A HtmlHelper extension method that fluents.</summary>
		///<remarks>Mhines, 11/4/2012.</remarks>
		///<param name="html">The HtmlHelper.</param>
		///<param name="tagName">The name of the tag to build.</param>
		///<returns>A FluentTagBuilder object.</returns>
		public static FluentTagBuilder Fluent(this HtmlHelper html, string tagName) {
			return new FluentTagBuilder(tagName);
		}

		///<summary>A HtmlHelper&lt;TModel&gt; extension method that fluents.</summary>
		///<remarks>Mhines, 11/4/2012.</remarks>
		///<typeparam name="TModel">Type of the model.</typeparam>
		///<param name="html">The HtmlHelper.</param>
		///<param name="tagName">The name of the tag to build.</param>
		///<returns>A FluentTagBuilder object.</returns>
		public static FluentTagBuilder Fluent<TModel>(this HtmlHelper<TModel> html, string tagName) {
			return new FluentTagBuilder(tagName);
		}
	}
}