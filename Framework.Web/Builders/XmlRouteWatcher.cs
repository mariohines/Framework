using System;
using System.IO;

namespace Framework.Web.Builders
{
	///<summary>XML route watcher.</summary>
	///<remarks>Mhines, 11/29/2012.</remarks>
	public sealed class XmlRouteWatcher
	{
		private DateTime _timestamp;
		private readonly Action _callBack;
		private readonly string _filePath;

		///<summary>Constructor.</summary>
		///<remarks>Mhines, 11/29/2012.</remarks>
		///<param name="filePath">Full pathname of the file.</param>
		///<param name="callBack">The call back.</param>
		private XmlRouteWatcher(string filePath, Action callBack) {
			_callBack = callBack;
			_filePath = filePath;
		}

		///<summary>Listen for changes.</summary>
		///<remarks>Mhines, 11/29/2012.</remarks>
		private void ListenForChanges() {
			var directory = Path.GetDirectoryName(_filePath);
			if (string.IsNullOrEmpty(directory)) return;
			var watcher = new FileSystemWatcher(directory) {
				EnableRaisingEvents = true,
				NotifyFilter = NotifyFilters.LastWrite
			};
			watcher.Changed += (sender, args) => {
				if (_timestamp <= DateTime.UtcNow.AddSeconds(-1)) return;
				_callBack();
				_timestamp = DateTime.UtcNow;
			};
		}

		///<summary>Listens the given file.</summary>
		///<remarks>Mhines, 11/29/2012.</remarks>
		///<param name="filePath">Full pathname of the file.</param>
		///<param name="callBack">The call back.</param>
		public static void Listen(string filePath, Action callBack) {
			var watcher = new XmlRouteWatcher(filePath, callBack);
			watcher.ListenForChanges();
		}
	}
}