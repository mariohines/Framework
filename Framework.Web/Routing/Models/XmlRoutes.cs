using System.Collections.Generic;
using System.Xml.Serialization;

namespace Framework.Web.Routing.Models
{
	///<summary>XML routes.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	[XmlRoot("routes")]
	public class XmlRoutes
	{
		///<summary>Gets or sets the ignored routes.</summary>
		///<value>The ignored routes.</value>
		[XmlElement("ignore")]
		public List<XmlIgnoreRoute> IgnoredRoutes { get; set; }

		///<summary>Gets or sets the routes.</summary>
		///<value>The routes.</value>
		[XmlElement("route")]
		public List<XmlRoute> Routes { get; set; }
	}
}
