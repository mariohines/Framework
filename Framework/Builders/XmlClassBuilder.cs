using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using Framework.Extensions;
using Framework.Interfaces;

namespace Framework.Builders
{
	///<summary>XML class builder.</summary>
	///<remarks>Mhines, 11/24/2012.</remarks>
	public class XmlClassBuilder : IXmlSerializer
	{
		private readonly XmlWriterSettings _settings;
		private readonly XslCompiledTransform _xsltTransformer;
		private const string NamespaceExpression = "\\sxmlns:\\w+=\".*\"";

		///<summary>Default constructor.</summary>
		public XmlClassBuilder(Encoding encoding = null, bool canIndent = true, NewLineHandling lineHandling = NewLineHandling.Entitize) {
			_settings = new XmlWriterSettings {
				Encoding = encoding ?? Encoding.UTF8,
				Indent = canIndent,
				NamespaceHandling = NamespaceHandling.OmitDuplicates,
				NewLineHandling = lineHandling
			};
			_xsltTransformer = new XslCompiledTransform(true);

		}

		#region Implementation of IXmlSerializer

		///<summary>Method to return the xml format of <typeparamref name="TSource"/> to a string.</summary>
		///<remarks>Mhines, 11/24/2012.</remarks>
		///<typeparam name="TSource">The source type.</typeparam>
		///<param name="source">The type to serialize as xml.</param>
		///<param name="omitDeclaration">Either use declaration or not.</param>
		///<param name="omitNamespaces"> (optional) the omit namespaces.</param>
		///<returns>An xml string.</returns>
		public string ToXmlString<TSource>(TSource source, bool omitDeclaration = false, bool omitNamespaces = true) where TSource : class, new() {
			var builder = new StringBuilder();
			_settings.OmitXmlDeclaration = omitDeclaration;
			using (var writer = XmlWriter.Create(builder, _settings)) {
				var serializer = new XmlSerializer(typeof(TSource));
				if (source == null) {
					throw new ArgumentNullException("source");
				}
				serializer.Serialize(writer, source);
				writer.Flush();
			}
			var output = omitNamespaces
							 ? Regex.Replace(builder.ToString(), NamespaceExpression, string.Empty)
							 : builder.ToString();
			return output;
		}

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a string using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transformation.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		public string ToXmlStringWithTransform<TSource>(TSource source, string transform, bool omitDeclaration = false) where TSource : class, new() {
			var serializedOutput = new StringBuilder();
			var transformOutput = new StringBuilder();
			_settings.OmitXmlDeclaration = omitDeclaration;
			using (var writer = XmlWriter.Create(serializedOutput, _settings)) {
				var serializer = new XmlSerializer(typeof(TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
			}
			using (var writer = XmlWriter.Create(transformOutput)) {
				_xsltTransformer.Load(transform);
				_xsltTransformer.Transform(serializedOutput.ToString(), writer);
				writer.Flush();
			}
			return transformOutput.ToString();
		}

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a string using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transformation.</param>
		/// <param name="omitDeclaration">Either use declaration or not.</param>
		/// <returns>An xml string.</returns>
		public string ToXmlStringWithTransform<TSource>(TSource source, XmlReader transform, bool omitDeclaration = false) where TSource : class, new() {
			var serializedOutput = new StringBuilder();
			var transformOutput = new StringBuilder();
			_settings.OmitXmlDeclaration = omitDeclaration;
			using (var writer = XmlWriter.Create(serializedOutput, _settings)) {
				var serializer = new XmlSerializer(typeof(TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
			}
			using (var writer = XmlWriter.Create(transformOutput)) {
				_xsltTransformer.Load(transform);
				_xsltTransformer.Transform(serializedOutput.ToString(), writer);
				writer.Flush();
			}
			return transformOutput.ToString();
		}

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a stream.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <returns>An generic stream containing xml.</returns>
		public Stream ToXmlStream<TSource>(TSource source) where TSource : class, new() {
			var stream = new MemoryStream();
			using (var writer = XmlWriter.Create(stream, _settings)) {
				var serializer = new XmlSerializer(typeof (TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
				stream.Seek(0, SeekOrigin.Begin);
			}
			return stream;
		}

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a stream using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <returns>A generic stream containing xml.</returns>
		public Stream ToXmlStreamWithTransform<TSource>(TSource source, string transform) where TSource : class, new() {
			var serializedStream = new MemoryStream();
			var transformedStream = new MemoryStream();
			using (var writer = XmlWriter.Create(serializedStream, _settings)) {
				var serializer = new XmlSerializer(typeof (TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
				serializedStream.Seek(0, SeekOrigin.Begin);
			}
			using (var writer = XmlWriter.Create(transformedStream, _settings)) {
				_xsltTransformer.Load(transform);
				_xsltTransformer.Transform(XmlReader.Create(serializedStream), writer);
				writer.Flush();
				transformedStream.Seek(0, SeekOrigin.Begin);
			}
			return transformedStream;
		}

		/// <summary>Method to return the xml format of <typeparamref name="TSource"/> to a stream using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <returns>A generic stream containing xml.</returns>
		public Stream ToXmlStreamWithTransform<TSource>(TSource source, XmlReader transform) where TSource : class, new() {
			var serializedStream = new MemoryStream();
			var transformedStream = new MemoryStream();
			using (var writer = XmlWriter.Create(serializedStream, _settings)) {
				var serializer = new XmlSerializer(typeof(TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
				serializedStream.Seek(0, SeekOrigin.Begin);
			}
			using (var writer = XmlWriter.Create(transformedStream, _settings)) {
				_xsltTransformer.Load(transform);
				_xsltTransformer.Transform(XmlReader.Create(serializedStream), writer);
				writer.Flush();
				transformedStream.Seek(0, SeekOrigin.Begin);
			}
			return transformedStream;
		}

		/// <summary>Method to return the xml format of a <typeparamref name="TSource"/> to a file.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		public void ToXmlFile<TSource>(TSource source, string filePath = null) where TSource : class, new() {
			var sourceType = typeof (TSource);
			var customPath = filePath.HasValue() ? filePath : Assembly.GetAssembly(sourceType).Location;
			if (string.IsNullOrWhiteSpace(customPath)) {
				throw new InvalidOperationException("An error has occured.");
			}
			using (var writer = XmlWriter.Create(customPath, _settings)) {
				var serializer = new XmlSerializer(typeof (TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
			}
		}

		/// <summary>Method to return the xml format of a <typeparamref name="TSource"/> to a file using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		public void ToXmlFileWithTransform<TSource>(TSource source, string transform, string filePath = null) where TSource : class, new() {
			var sourceType = typeof (TSource);
			var customPath = filePath.HasValue() ? filePath : Assembly.GetAssembly(sourceType).Location;
			if (string.IsNullOrWhiteSpace(customPath)) {
				throw new InvalidOperationException("An error has occured.");
			}
			using (var writer = XmlWriter.Create(customPath, _settings)) {
				var serializer = new XmlSerializer(typeof (TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
			}
			using (var writer = XmlWriter.Create(customPath, _settings)) {
				_xsltTransformer.Load(transform);
				_xsltTransformer.Transform(XmlReader.Create(customPath), writer);
				writer.Flush();
			}
		}

		/// <summary>Method to return the xml format of a 
		/// <typeparamref name="TSource"/> to a file using an xslt transform.</summary>
		/// <typeparam name="TSource">The source type.</typeparam>
		/// <param name="source">The type to serialize as xml.</param>
		/// <param name="transform">The xslt used for the transform.</param>
		/// <param name="filePath">The file path of where to dump the xml.</param>
		public void ToXmlFileWithTransform<TSource>(TSource source, XmlReader transform, string filePath = null) where TSource : class, new() {
			var sourceType = typeof(TSource);
			var customPath = filePath.HasValue() ? filePath : Assembly.GetAssembly(sourceType).Location;
			if (string.IsNullOrWhiteSpace(customPath)) {
				throw new InvalidOperationException("An error has occured.");
			}
			using (var writer = XmlWriter.Create(customPath, _settings)) {
				var serializer = new XmlSerializer(typeof(TSource));
				serializer.Serialize(writer, source);
				writer.Flush();
			}
			using (var writer = XmlWriter.Create(customPath, _settings)) {
				_xsltTransformer.Load(transform);
				_xsltTransformer.Transform(XmlReader.Create(customPath), writer);
				writer.Flush();
			}
		}

		#endregion
	}
}