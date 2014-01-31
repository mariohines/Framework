using System.IO;
using System.Xml;
using System.Xml.Linq;
using Framework.Builders;

namespace Framework.Extensions
{
	public static partial class Extensions
	{
		///<summary>Extension method to check if the XElement value is empty..</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<param name="element">The element to act on.</param>
		///<param name="elementName">Name of the element.</param>
		///<returns>An XElement.</returns>
		public static XElement EmptyIfNull(this XElement element, string elementName) {
			return element ?? new XElement(elementName) {Value = string.Empty};
		}

		/// <summary>Extension method to return the xml format of <typeparamref name="TSource"/> to a string.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		public static string ToXmlString<TSource> (this TSource source, bool omitDeclaration = false) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			return builder.ToXmlString(source, omitDeclaration);
		}

		/// <summary>Extension method to return the xml format of <typeparamref name="TSource"/> to a string using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transformation.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		public static string ToXmlStringWithTransform<TSource> (this TSource source, string transform, bool omitDeclaration = false) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			return builder.ToXmlStringWithTransform(source, transform, omitDeclaration);
		}

		/// <summary>Extension method to return the xml format of <typeparamref name="TSource"/> to a string using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transformation.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		public static string ToXmlStringWithTransform<TSource> (this TSource source, XmlReader transform, bool omitDeclaration = false) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			return builder.ToXmlStringWithTransform(source, transform, omitDeclaration);
		}

		/// <summary>Extension method to return the xml format of <typeparamref name="TSource"/> to a stream.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <returns>An generic stream containing xml.</returns>
		public static Stream ToXmlStream<TSource>(this TSource source) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			return builder.ToXmlStream(source);
		}

		/// <summary>Extension method to return the xml format of <typeparamref name="TSource"/> to a stream using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <returns>A generic stream containing xml.</returns>
		public static Stream ToXmlStreamWithTransform<TSource>(this TSource source, string transform) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			return builder.ToXmlStreamWithTransform(source, transform);
		}

		/// <summary>Extension method to return the xml format of <typeparamref name="TSource"/> to a stream using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <returns>A generic stream containing xml.</returns>
		public static Stream ToXmlStreamWithTransform<TSource> (this TSource source, XmlReader transform) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			return builder.ToXmlStreamWithTransform(source, transform);
		}

		/// <summary>Method to return the xml format of a <typeparamref name="TSource"/> to a file.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		public static void ToXmlFile<TSource>(this TSource source, string filePath = null) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			builder.ToXmlFile(source, filePath);
		}

		/// <summary>Extension method to return the xml format of a <typeparamref name="TSource"/> to a file using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		public static void ToXmlFileWithTransform<TSource>(this TSource source, string transform, string filePath = null) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			builder.ToXmlFileWithTransform(source, transform, filePath);
		}

		/// <summary>Extension method to return the xml format of a <typeparamref name="TSource"/> to a file using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		public static void ToXmlFileWithTransform<TSource> (this TSource source, XmlReader transform, string filePath = null) where TSource : class, new() {
			var builder = new XmlClassBuilder();
			builder.ToXmlFileWithTransform(source, transform, filePath);
		}
	}
}