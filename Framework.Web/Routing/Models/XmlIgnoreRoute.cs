using System.Web.Routing;
using System.Xml.Serialization;
using Framework.Core.Extensions;

namespace Framework.Web.Routing.Models
{
	///<summary>XML ignore route.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	[XmlRoot("ignore")]
	[XmlType("ignore")]
	public class XmlIgnoreRoute
	{
		///<summary>Gets or sets URL of the document.</summary>
		///<value>The URL.</value>
		[XmlElement("url")]
		public string Url { get; set; }

		///<summary>Gets or sets the constraints.</summary>
		///<value>The constraints.</value>
		[XmlElement("constraints")]
		public XmlRouteConstraint Constraints { get; set; }

		///<summary>RouteBase casting operator.</summary>
		///<remarks>Mhines, 11/29/2012.</remarks>
		///<param name="ignoreRoute">The ignore route.</param>
		public static explicit operator RouteBase(XmlIgnoreRoute ignoreRoute) {
			var route = new LowercaseRoute(ignoreRoute.Url, new StopRoutingHandler());
			if (!ignoreRoute.Constraints.IsNull()) {
				route.Constraints = new RouteValueDictionary(ignoreRoute.Constraints.ConstraintDictionary);
			}
			return route;
		}
	}
}
