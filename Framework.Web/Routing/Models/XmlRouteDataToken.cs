using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Framework.Core.Extensions;

namespace Framework.Web.Routing.Models
{
	///<summary>Defines the XML representation of 'datatokens' node in a xml routing file.</summary>
	///<remarks>Mhines, 11/4/2012.</remarks>
	[XmlRoot("datatokens")]
	[XmlType("datatokens")]
	public class XmlRouteDataToken
	{
		private Dictionary<string, object> _constraints;

		/// <summary>Gets/sets the collection of elements held in the constraints node.</summary>
		[XmlAnyElement]
		public List<XmlElement> Elements { get; set; }

		/// <summary>Gets the collection of elements and transforms them into a dictionary.</summary>
		[XmlIgnore]
		public Dictionary<string, object> TokenDictionary {
			get { return _constraints ?? (_constraints = Elements.ToDictionary(key => key.Name, GetElementValue)); }
		}

		private object GetElementValue (XmlElement element) {
			var value = (object)element.Value;
			if (value.IsNull()) {
				value = element.InnerText;
			}
			return value;
		} 
	}
}