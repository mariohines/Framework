using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Framework.Extensions;

namespace Framework.Web.Routing.Models
{
	/// <summary>Defines the XML representation of 'defaults' node in a xml routing file.</summary>
	[XmlRoot("defaults")]
	[XmlType("defaults")]
	public class XmlRouteDefault
	{
		private Dictionary<string, object> _defaults;

		/// <summary>Gets/sets the collection of elements held in the defaults node.</summary>
		[XmlAnyElement]
		public List<XmlElement> Elements { get; set; }

		/// <summary>Gets the collection of elements and transforms them into a dictionary.</summary>
		[XmlIgnore]
		public Dictionary<string, object> DefaultDictionary {
			get { return _defaults ?? (_defaults = Elements.ToDictionary(key => key.Name, GetElementValue)); }
		}

		private object GetElementValue (XmlElement element) {
			var value = (object)element.Value;
			if (value.IsNull()) {
				if (element.InnerText.Equals("{optional}", StringComparison.OrdinalIgnoreCase)) {
					value = UrlParameter.Optional;
				} else {
					value = element.InnerText;
				}
			}
			return value;
		}
	}
}
