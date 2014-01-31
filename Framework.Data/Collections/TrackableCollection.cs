using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Framework.Data.Collections
{
	/// <summary>Collection of trackables.</summary>
	/// <typeparam name="T">Generic type parameter.</typeparam>
	public class TrackableCollection<T> : ObservableCollection<T>
	{
		/// <summary>Clears the items.</summary>
		protected override void ClearItems () {
			new List<T>(this).ForEach(t => Remove(t));
		}

		/// <summary>Inserts an item.</summary>
		/// <param name="index">Zero-based index of the.</param>
		/// <param name="item">The item.</param>
		protected override void InsertItem (int index, T item) {
			if (!Contains(item)) {
				base.InsertItem(index, item);
			}
		}
	}
}