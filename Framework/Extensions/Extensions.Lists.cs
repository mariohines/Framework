using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LinqKit;

namespace Framework.Extensions
{
	public static partial class Extensions
	{
		#region Extension Methods for IEnumerable<string>

		/// <summary>Extension method to prefix all strings in an IEnumerable of type string.</summary>
		/// <param name="source">The source IEnumerable of type string.</param>
		/// <param name="prefix">The string prefix.</param>
		/// <returns>An IEnumerable of type string.</returns>
		public static IEnumerable<string> PrefixAll (this IEnumerable<string> source, string prefix) {
			var list = new List<string>();
			source.ForEach(item => list.Add(string.Format("{0}{1}", prefix, item)));
			return list;
		}

		/// <summary>Extension method to suffix all strings in an IEnumerable of type string.</summary>
		/// <param name="source">The source IEnumerable of type string.</param>
		/// <param name="suffix">The string suffix.</param>
		/// <returns>An IEnumerable of type string.</returns>
		public static IEnumerable<string> SuffixAll (this IEnumerable<string> source, string suffix) {
			var list = new List<string>();
			source.ForEach(item => list.Add(string.Format("{0}{1}", item, suffix)));
			return list;
		}

		/// <summary>Extension method to delimit the strings of an IEnumerable of type string into 1 string.</summary>
		/// <param name="source">The source IEnumerable of type string.</param>
		/// <param name="delimeter">The delimiter to use. [Default is \r\n]</param>
		/// <returns>A string.</returns>
		public static string DelimitAllToString (this IEnumerable<string> source, string delimeter = "\r\n") {
			return string.Join(delimeter, source);
		}

		#endregion

		#region Extension Methods for class

		/// <summary>Extension method to convert an IEnumerable of type <typeparamref name="TSource"/> to a datatable.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The source IEnumerable of type <typeparamref name="TSource"/>.</param>
		/// <returns>A DataTable.</returns>
		public static DataTable ToDataTable<TSource> (this IEnumerable<TSource> source) where TSource : class {
			var table = GetDataTableOfType<TSource>();
			source.ForEach(item => table.Rows.Add(item.ToDataRow(table)));
			return table;
		}

		/// <summary>Extension method to convert an IEnumerable of type <typeparamref name="TSource"/> to a byte array for CSV.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The source IEnumerable of type <typeparamref name="TSource"/>.</param>
		/// <returns>A byte array for CSV.</returns>
		public static byte[] ToCsv<TSource> (this IEnumerable<TSource> source) where TSource : class {
			var table = source.ToDataTable();
			var encoding = new ASCIIEncoding();
			var builder = new StringBuilder();
			var columnNames = table.Columns.SafeCast<DataColumn>().Select(column => column.ColumnName);

			builder.AppendLine(string.Join(",", columnNames));

			for (var i = 0; i < table.Rows.Count; i++) {
				var rowData = string.Join(",", table.Rows[i].ItemArray.SafeCast<string>());
				builder.AppendLine(rowData);
			}
			return encoding.GetBytes(builder.ToString());
		}

		#endregion End Extension Methods for class
	}
}