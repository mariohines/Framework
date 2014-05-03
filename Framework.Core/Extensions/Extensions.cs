using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Framework.Core.Interfaces;

namespace Framework.Core.Extensions
{
	/// <summary>Collection of commonly used extensions.</summary>
	public static partial class Extensions
	{
		private static readonly ParallelOptions Options;
		private const string SystemString = "System";
		private const string MscorlibString = "mscorlib";

		static Extensions() {
			Options = new ParallelOptions
						  {
							  MaxDegreeOfParallelism = Environment.ProcessorCount <= 4 ? Environment.ProcessorCount/2 : Environment.ProcessorCount
						  };
		}

		///<summary>Extension method to convert an object to a data row.</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<typeparam name="TSource">The source type.</typeparam>
		///<param name="source">The input source.</param>
		///<param name="dataTable">The parent DataTable. [Optional].</param>
		///<returns>The given data converted to a DataRow.</returns>
		public static DataRow ToDataRow<TSource>(this TSource source, DataTable dataTable = null) where TSource : class {
			var row = (dataTable ?? GetDataTableOfType<TSource>()).NewRow();
			foreach (
				var property in typeof (TSource).GetProperties().Where(property => IsValidType(property.PropertyType))) {
				row[property.Name] = property.GetValue(source, null);
			}

			return row;
		}

		/// <summary>Extension method on a DataTable to return a list of DataRows.</summary>
		/// <param name="table">The source DataTable.</param>
		/// <returns>An IEnumerable of type DataRow.</returns>
		public static IEnumerable<DataRow> ToRowList(this DataTable table) {
			return table.Rows.SafeCast<DataRow>();
		}

		/// <summary>An Enum extension method that gets a value.</summary>
		/// <typeparam name="TTarget">Generic type parameter.</typeparam>
		/// <param name="e">The enumeration to act on.</param>
		/// <returns>The value of the enumeration.</returns>
		public static TTarget GetValue<TTarget>(this Enum e) {
			var names = Enum.GetNames(e.GetType());
			var name = Enum.GetName(e.GetType(), e);
			var index = names.ToList().IndexOf(name);
			return (TTarget) Convert.ChangeType(Enum.GetValues(e.GetType()).GetValue(index), typeof (TTarget));
		}

		/// <summary>An Enum extension method that gets a display.</summary>
		/// <param name="e">The enumeration to act on.</param>
		/// <returns>The display.</returns>
		public static string GetDisplay(this Enum e) {
			var members = e.GetType().GetMember(e.ToString());
			var display = members.Any()
				? members.First().GetCustomAttribute(typeof (DescriptionAttribute), false) as DescriptionAttribute
				: null;
			return display == null ? string.Empty : display.Description;
		}

		/// <summary>An IDependencyInjector extension method that configure by convention.</summary>
		/// <param name="injector">The injector to act on.</param>
		/// <param name="bindAll">If true, binds all assemblies in the folder. (Optional) Defaults to false.</param>
		/// <param name="excludedAssemblies">The exact names of assemblies to exclude if <paramref name="bindAll"/> is set to true.</param>
		/// <remarks>By default...all System libraries as well as mscorlib is excluded.</remarks>
		public static void ConfigureByConvention(this IDependencyInjector injector, bool bindAll = false, params string[] excludedAssemblies) {
			var assembly = Assembly.GetCallingAssembly();
			if (!bindAll) {
				assembly.BindAssembly(injector);
				return;
			}
			var assemblies = new List<Assembly>{ assembly };
			var exclusions = new List<string> {MscorlibString};
			exclusions.AddRange(excludedAssemblies);
			var assemblyNames = assembly.GetReferencedAssemblies().Where(asm => !asm.Name.StartsWith(SystemString) && !exclusions.Contains(asm.Name)).ToList();
			assemblyNames.ForEach(asm =>
								  {
									  var loopAssembly = Assembly.Load(asm);
									  if (loopAssembly != null) {
										  assemblies.Add(loopAssembly);
									  }
								  });
			assemblies.BindAssemblies(injector);
		}

		/// <summary>An object extension method that toes the given source.</summary>
		/// <typeparam name="TTarget">Type of the type.</typeparam>
		/// <param name="source">The input source.</param>
		/// <returns>The object converted to the chosen type.</returns>
		public static TTarget To<TTarget>(this object source) {
			return (TTarget) Convert.ChangeType(source, typeof (TTarget));
		}

		#region Private Methods

		private static DataTable GetDataTableOfType<TSource>() where TSource : class {
			var table = new DataTable(string.Format("{0}{1}", typeof (TSource).Name, "DataTable"));
			foreach (var property in typeof (TSource).GetProperties().Where(property => IsValidType(property.PropertyType))) {
				table.Columns.Add(property.Name, property.PropertyType);
			}
			return table;
		}

		private static bool IsValidType(Type info) {
			var obj = (info.IsValueType || info.IsAnsiClass) && !info.IsGenericType && !info.IsArray;
			return obj;
		}

		#endregion End Private Methods
	}
}