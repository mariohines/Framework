using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;
using Framework.Core.Extensions;

namespace Framework.Web.Routing.Models
{
	///<summary>XML route.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	[XmlRoot("route")]
	[XmlType("route")]
	public class XmlRoute
	{
		///<summary>Gets or sets the area.</summary>
		///<value>The area.</value>
		[XmlAttribute("area")]
		public string Area { get; set; }

		///<summary>Gets or sets the name.</summary>
		///<value>The name.</value>
		[XmlElement("name")]
		public string Name { get; set; }

		///<summary>Gets or sets URL of the document.</summary>
		///<value>The URL.</value>
		[XmlElement("url")]
		public string Url { get; set; }

		///<summary>Gets or sets the defaults.</summary>
		///<value>The defaults.</value>
		[XmlElement("defaults")]
		public XmlRouteDefault Defaults { get; set; }

		///<summary>Gets or sets the constraints.</summary>
		///<value>The constraints.</value>
		[XmlElement("constraints")]
		public XmlRouteConstraint Constraints { get; set; }

		/// <summary>Gets or sets the data tokens.</summary>
		/// <value>The data tokens.</value>
		[XmlElement("datatokens")]
		public XmlRouteDataToken DataTokens { get; set; }

		///<summary>Gets or sets the namespaces.</summary>
		///<value>The namespaces.</value>
		[XmlElement("namespaces")]
		public string Namespaces { get; set; }

		///<summary>RouteBase casting operator.</summary>
		///<remarks>Mhines, 11/4/2012.</remarks>
		///<param name="xmlRoute">The XmlRoute to convert from.</param>
		///<returns>A RouteBase object.</returns>
		public static explicit operator RouteBase(XmlRoute xmlRoute) {
			var route = new LowercaseRoute(xmlRoute.Url, new MvcRouteHandler());
			if (!xmlRoute.Defaults.IsNull()) {
				route.Defaults = new RouteValueDictionary(xmlRoute.Defaults.DefaultDictionary);
			}
			if (!xmlRoute.Constraints.IsNull()) {
				route.Constraints = new RouteValueDictionary(xmlRoute.Constraints.ConstraintDictionary);
			}
			if (!xmlRoute.DataTokens.IsNull()) {
				route.DataTokens = new RouteValueDictionary(xmlRoute.DataTokens.TokenDictionary);
			}
			if(xmlRoute.Area.HasValue()) {
				route.DataTokens.Add("Area", xmlRoute.Area);
			}
			if (xmlRoute.Namespaces.HasValue()) {
				route.DataTokens.Add("Namespaces", xmlRoute.Namespaces.Split(','));
			}
			return route;
		}
	}
}
