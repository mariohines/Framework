using System.Collections.Generic;
using System.Configuration;
using Framework.Core.Collections;

namespace Framework.Core.Configuration
{
	/// <summary>A dynamic class for handling AppSetting and ConnectionStrings.</summary>
	public static class DynamicConfigurationManager
	{
		static DynamicConfigurationManager() {
			AppSettings = new DynamicCollection(ConfigurationManager.AppSettings);
			var collection = new Dictionary<string, object>();
			foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings) {
				collection[connectionString.Name] = connectionString.ConnectionString;
			}
			ConnectionStrings = new DynamicCollection(collection);
		}

		/// <summary>Dynamic AppSettings Property.</summary>
		public static dynamic AppSettings { get; private set; }

		/// <summary>Dynamic ConnectionStrings Property.</summary>
		public static dynamic ConnectionStrings { get; private set; }
	}
}