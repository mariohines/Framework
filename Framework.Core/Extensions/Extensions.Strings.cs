using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using LinqKit;

namespace Framework.Core.Extensions
{
	public static partial class Extensions
	{
		/// <summary>Extension method to properly case a string.</summary>
		/// <param name="source">The source string.</param>
		/// <returns>The properly cased string.</returns>
		public static string ToProperCase(this string source) {
			var culture = CultureInfo.CurrentCulture;
			return culture.TextInfo.ToTitleCase(source.ToLower());
		}

		/// <summary>Extension method to verify if a string has a value.</summary>
		/// <param name="source">The source string.</param>
		/// <returns>A boolean value.</returns>
		public static bool HasValue(this string source) {
			return !string.IsNullOrWhiteSpace(source);
		}

		/// <summary>A string extension method that returns null if empty or whitespace.</summary>
		/// <param name="source">The source string.</param>
		/// <returns>A string.</returns>
		public static string NullIfEmpty(this string source) {
			return source.HasValue() ? source : null;
		}

		/// <summary>Extension method to do a mass replace of old strings with a new string.</summary>
		/// <param name="source">The source string.</param>
		/// <param name="oldValues">The string array of old values.</param>
		/// <param name="newValue">The replacement string.</param>
		/// <returns>The formatted string.</returns>
		public static string ReplaceAll(this string source, string[] oldValues, string newValue) {
			return oldValues.Aggregate(source, (current, oldValue) => current.Replace(oldValue, newValue));
		}

		/// <summary>Extension method to do a mass replace of old characters with a new character.</summary>
		/// <param name="source">The source string.</param>
		/// <param name="oldValues">The character array of old values.</param>
		/// <param name="newValue">The replacement character.</param>
		/// <returns>The formatted string.</returns>
		public static string ReplaceAll(this string source, char[] oldValues, char newValue) {
			return oldValues.Aggregate(source, (current, oldValue) => current.Replace(oldValue, newValue));
		}

		///<summary>Extension method that is a shortcut for the string.FormatWith method.</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<param name="source">The source string.</param>
		///<param name="replacements">A variable-length parameters list containing replacements.</param>
		///<returns>The formatted with string.</returns>
		public static string FormatWith(this string source, params object[] replacements) {
			return replacements.Count() == 1 ? string.Format(source, replacements[0]) : string.Format(source, replacements);
		}

		///<summary>Extension method that is a shortcut for the string.Join method.</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<param name="source">The source to act on.</param>
		///<param name="separator">(optional) the separator.</param>
		///<returns>The joined string.</returns>
		public static string Join(this IEnumerable<string> source, string separator = "\r\n") {
			return string.Join(separator, source);
		}

		/// <summary>Extension method to append characters/strings at the end of a string.</summary>
		/// <param name="source">The source string.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The appended string.</returns>
		public static string Append(this string source, params object[] parameters) {
			var builder = new StringBuilder(source);
			parameters.ForEach(p => builder.Append(p));
			return builder.ToString();
		}

		/// <summary>Extension method to prepend characters/strings at the end of a string.</summary>
		/// <param name="source">The source string.</param>
		/// <param name="parameters">Options for controlling the operation.</param>
		/// <returns>The prepended string.</returns>
		public static string Prepend(this string source, params object[] parameters) {
			var builder = new StringBuilder();
			parameters.ForEach(p => builder.Append(p));
			return builder.Append(source).ToString();
		}

		/// <summary>A string extension method that coalesces.</summary>
		/// <param name="value">The value to act on.</param>
		/// <param name="comparers">A variable-length parameters list containing comparers.</param>
		/// <returns>A string.</returns>
		public static string Coalesce(this string value, params string[] comparers) {
			return value.HasValue() ? value : comparers.FirstOrDefault(comparer => comparer.HasValue());
		}
	}
}