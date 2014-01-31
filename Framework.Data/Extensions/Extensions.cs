using System;
using System.IO;
using System.Linq;
using Framework.Data.Enumerations;
using Framework.Data.Interfaces;

namespace Framework.Data.Extensions
{
    /// <summary>Extensions.</summary>
	public static partial class Extensions
	{
        /// <summary>A T extension method that mark as deleted.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="trackingItem">The trackingItem to act on.</param>
        /// <returns>.</returns>
		public static T MarkAsDeleted<T> (this T trackingItem) where T : IObjectWithChangeTracker {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
			trackingItem.ChangeTracker.State = ObjectState.Deleted;
			return trackingItem;
		}

        /// <summary>A T extension method that mark as added.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="trackingItem">The trackingItem to act on.</param>
        /// <returns>.</returns>
		public static T MarkAsAdded<T> (this T trackingItem) where T : IObjectWithChangeTracker {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
			trackingItem.ChangeTracker.State = ObjectState.Added;
			return trackingItem;
		}

        /// <summary>A T extension method that mark as modified.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="trackingItem">The trackingItem to act on.</param>
        /// <returns>.</returns>
		public static T MarkAsModified<T> (this T trackingItem) where T : IObjectWithChangeTracker {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
			trackingItem.ChangeTracker.State = ObjectState.Modified;
			return trackingItem;
		}

        /// <summary>A T extension method that mark as unchanged.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="trackingItem">The trackingItem to act on.</param>
        /// <returns>.</returns>
		public static T MarkAsUnchanged<T> (this T trackingItem) where T : IObjectWithChangeTracker {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
			trackingItem.ChangeTracker.State = ObjectState.Unchanged;
			return trackingItem;
		}

        /// <summary>An IObjectWithChangeTracker extension method that starts a tracking.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="trackingItem">The trackingItem to act on.</param>
		public static void StartTracking (this IObjectWithChangeTracker trackingItem) {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.ChangeTrackingEnabled = true;
		}

        /// <summary>An IObjectWithChangeTracker extension method that stops a tracking.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="trackingItem">The trackingItem to act on.</param>
		public static void StopTracking (this IObjectWithChangeTracker trackingItem) {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.ChangeTrackingEnabled = false;
		}

        /// <summary>An IObjectWithChangeTracker extension method that accept changes.</summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="trackingItem">The trackingItem to act on.</param>
		public static void AcceptChanges (this IObjectWithChangeTracker trackingItem) {
			if (trackingItem == null) {
				throw new ArgumentNullException("trackingItem");
			}

			trackingItem.ChangeTracker.AcceptChanges();
		}

        /// <summary>A DirectoryInfo extension method that gets an used space as string.</summary>
        /// <param name="info">.</param>
        /// <returns>The used space as string.</returns>
		public static string GetUsedSpaceAsString (this DirectoryInfo info) {
			long sizeInBytes = info.GetFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
			var unit = new UnitOfSpace((ulong)sizeInBytes);

			return unit.ToString();
		}

        /// <summary>A DirectoryInfo extension method that gets an used space.</summary>
        /// <param name="info">.</param>
        /// <returns>The used space.</returns>
		public static UnitOfSpace GetUsedSpace (this DirectoryInfo info) {
			long sizeInBytes = info.GetFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);

			return new UnitOfSpace((ulong)sizeInBytes);
		}
	}
}
