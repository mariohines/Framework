using System.Configuration;

namespace Framework.Web.Configuration
{
	///<summary>XML routing configuration.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public sealed class XmlRoutingConfiguration : ConfigurationSection
	{
		#region Fields
		private static readonly ConfigurationProperty RouteFile;
		private static readonly ConfigurationProperty IsCached;
		private static readonly ConfigurationPropertyCollection Property; 
		#endregion

		#region Properties
		///<summary>Gets the collection of properties.</summary>
		///<value>
		///The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.
		///</value>
		protected override ConfigurationPropertyCollection Properties {
			get { return Property; }
		}

		///<summary>Gets or sets the route file location.</summary>
		///<value>The route file location.</value>
		public string RouteFileLocation {
			get { return (string) this["routingFile"]; }
			set { this["routingFile"] = value; }
		}

		///<summary>Gets or sets a value indicating whether this object is routing cached.</summary>
		///<value>true if this object is routing cached, false if not.</value>
		public bool IsRoutingCached {
			get { return (bool) this["isCached"]; }
			set { this["isCached"] = value; }
		}

		#endregion

		static XmlRoutingConfiguration() {
			RouteFile = new ConfigurationProperty("routeFile", typeof (string), @"App_Data\routes.xml",
			                                      ConfigurationPropertyOptions.None);
			IsCached = new ConfigurationProperty("isCached", typeof (bool), false, ConfigurationPropertyOptions.IsRequired);
			Property = new ConfigurationPropertyCollection {RouteFile, IsCached};
		}
	}
}
