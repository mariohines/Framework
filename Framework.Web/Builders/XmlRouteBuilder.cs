using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Routing;
using System.Xml;
using System.Xml.Serialization;
using Framework.Web.Configuration;
using Framework.Web.Routing.Models;

namespace Framework.Web.Builders
{
	///<summary>XML route builder.</summary>
	///<remarks>Mhines, 11/24/2012.</remarks>
	public sealed class XmlRouteBuilder
	{
		/// <summary>Method to build the routing table via an xml file.</summary>
		/// <param name="routeCollection">The collection of http routes.</param>
		public static void LoadRoutes(RouteCollection routeCollection) {
			var routeSection = ConfigurationManager.GetSection("routingConfiguration") as XmlRoutingConfiguration;
			if(routeSection == null) {
				throw new ApplicationException("No routingConfiguration section exists in the config file.");
			}

			// Clears the routing collection.
			routeCollection.Clear();

			var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, routeSection.RouteFileLocation);
			if (routeSection.IsRoutingCached) {
				XmlRouteWatcher.Listen(path, () => BuildRoutes(path, routeCollection));
			}
			BuildRoutes(path, routeCollection);
		}
		
		///<summary>Builds the routes.</summary>
		///<remarks>Mhines, 11/4/2012.</remarks>
		///<exception cref="ApplicationException">Thrown when an application error condition occurs.</exception>
		///<param name="path">Full pathname of the file.</param>
		///<param name="routeCollection">The collection of http routes.</param>
		private static void BuildRoutes(string path, ICollection<RouteBase> routeCollection) {
			XmlRoutes routes;
			using (var reader = XmlReader.Create(new FileStream(path, FileMode.Open))) {
				var serializer = new XmlSerializer(typeof (XmlRoutes));
				routes = serializer.Deserialize(reader) as XmlRoutes;
			}

			if (routes == null) {
				throw new ApplicationException("There was an error deserializing the routing file. Please verify that it follows the correct schema.");
			}

			// Handle all the ignored routes first.
			routes.IgnoredRoutes.ForEach(r => routeCollection.Add((Route) r));

			// Handle regular routes.
			routes.Routes.ForEach(r => routeCollection.Add((Route) r));
		}
	}
}