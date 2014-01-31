using System.IO;
using System.Xml;

namespace Framework.Interfaces
{
	/// <summary>Interface to implement custom class serialization.</summary>
	public interface IXmlSerializer
	{
		///<summary>Method to return the xml format of <typeparamref name="TSource"/> to a string.</summary>
		///<typeparam name="TSource">The source type.</typeparam>
		///<param name="source">				 The type to serialize as xml.</param>
		///<param name="omitDeclaration">Either use declaration or not.</param>
		///<param name="omitNamespaces"> (optional) the omit namespaces.</param>
		///<returns>An xml string.</returns>
		string ToXmlString<TSource>(TSource source, bool omitDeclaration = false, bool omitNamespaces = true) where TSource : class, new();

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a string using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transformation.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		string ToXmlStringWithTransform<TSource>(TSource source, string transform, bool omitDeclaration = false)
			where TSource : class, new();

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a string using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transformation.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		string ToXmlStringWithTransform<TSource> (TSource source, XmlReader transform, bool omitDeclaration = false)
			where TSource : class, new();

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a stream.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <returns>An generic stream containing xml.</returns>
		Stream ToXmlStream<TSource>(TSource source) where TSource : class, new();

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a stream using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <returns>A generic stream containing xml.</returns>
		Stream ToXmlStreamWithTransform<TSource> (TSource source, string transform) where TSource : class, new();

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a stream using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <returns>A generic stream containing xml.</returns>
		Stream ToXmlStreamWithTransform<TSource> (TSource source, XmlReader transform) where TSource : class, new();

		/// <summary>Method to return the xml format of a <typeparamref name="TSource"/> to a file.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		void ToXmlFile<TSource>(TSource source, string filePath = null) where TSource : class, new();

		/// <summary>Method to return the xml format of a <typeparamref name="TSource"/> to a file using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		void ToXmlFileWithTransform<TSource>(TSource source, string transform, string filePath = null)
			where TSource : class, new();

		/// <summary>Method to return the xml format of a <typeparamref name="TSource"/> to a file using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		void ToXmlFileWithTransform<TSource> (TSource source, XmlReader transform, string filePath = null)
			where TSource : class, new();
	}
}