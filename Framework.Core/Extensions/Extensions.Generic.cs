using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.Extensions
{
	public static partial class Extensions
	{
		#region Extension Methods for Generic

		/// <summary>Extension method to execute an action across a generically typed collection.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The source IEnumerable of type <typeparamref name="TSource"/>.</param>
		/// <param name="action">An action to be performed on each item in the collection.</param>
		public static void ParallelExecute<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
			var collection = source.ToList();
			collection.ThrowIfNull();
			action.ThrowIfNull();
			var concurrentCollection = new ConcurrentBag<TSource>(collection);
			Parallel.ForEach(concurrentCollection, Options, action);
		}

		/// <summary>Extension method to convert a null IEnumerable of type <typeparamref name="TSource"/> to an empty IEnumerable of type <typeparamref name="TSource"/>.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The source IEnumerable of type <typeparamref name="TSource"/>.</param>
		/// <returns>An IEnumerable of type <typeparamref name="TSource"/>.</returns>
		public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source) {
			return source ?? new List<TSource>().AsEnumerable();
		}

		/// <summary>Extension method to safely cast an IEnumerable to an IEnumerable of type <typeparamref name="TTarget"/>.</summary>
		/// <typeparam name="TTarget">The target type.</typeparam>
		/// <param name="source">The source IEnumerable of type <typeparamref name="TTarget"/>.</param>
		/// <returns>An IEnumerable of type <typeparamref name="TTarget"/>.</returns>
		public static IEnumerable<TTarget> SafeCast<TTarget>(this IEnumerable source) {
			return (from object item in source select (TTarget) Convert.ChangeType(item, typeof (TTarget)));
		}

		///<summary>Extension method that queries if 'source' is empty or null.</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<typeparam name="TSource">Type of the source.</typeparam>
		///<param name="source">The source IEnumerable of type <typeparamref name="TSource"/>.</param>
		///<returns>true if empty or null&lt; t source&gt;, false if not.</returns>
		public static bool IsEmptyOrNull<TSource>(this IEnumerable<TSource> source) {
			return source == null || !source.Any();
		}

		/// <summary>Enumerates as batch in this collection.</summary>
		/// <typeparam name="TSource">Type of the source.</typeparam>
		/// <param name="source">Source for the.</param>
		/// <param name="batchSize">Size of the batch.</param>
		/// <returns>An enumerator that allows foreach to be used to process as batch&lt; t source&gt; in this collection.</returns>
		public static IEnumerable<IEnumerable<TSource>> AsBatch<TSource>(this IEnumerable<TSource> source, int batchSize) {
			var collection = source.ToList();
			var totalSize = collection.Count;

			for (var start = 0; start < totalSize; start += batchSize) {
				yield return collection.Skip(start).Take(batchSize);
			}
		}

		/// <summary>Extension method on a class to validate if the class is null.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The input source.</param>
		/// <returns>A boolean value.</returns>
		public static bool IsNull<TSource>(this TSource source) where TSource : class {
			return (source is string) ? !(source as string).HasValue() : source == null;
		}

		/// <summary>Extension method to throw an exception if the source is null.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The input source.</param>
		public static void ThrowIfNull<TSource>(this TSource source) where TSource : class {
			if (source.IsNull()) {
				throw new ArgumentNullException("source");
			}
		}

		#endregion End Extension Methods for Generic
	}
}