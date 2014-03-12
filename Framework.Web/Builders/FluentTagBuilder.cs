using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Framework.Extensions;
using Framework.Web.Extensions;

namespace Framework.Web.Builders
{
	/// <summary>Class that implments a fluent architecture for building an html tag.</summary>
	public class FluentTagBuilder
	{
		private readonly TagBuilder _tagBuilder;

		#region Public Members

		/// <summary>Get the underlying TagBuilder's Attributes property.</summary>
		public IDictionary<string, string> Attributes {
			get { return _tagBuilder.Attributes; }
		}

		/// <summary>Get/Set the underlying TagBuilder's InnerHtml property.</summary>
		public string InnerHtml {
			get { return _tagBuilder.InnerHtml; }
			set { _tagBuilder.InnerHtml = value; }
		}

		#endregion End Public Members

		/// <summary>Constructor.</summary>
		/// <param name="tagName">Name of the tag.</param>
		public FluentTagBuilder(string tagName) {
			_tagBuilder = new TagBuilder(tagName);
		}

		#region Public Methods

		/// <summary>Method to add an attribute to the tag.</summary>
		/// <param name="key">The name of the tag.</param>
		/// <param name="value">The value of the tag.</param>
		/// <exception cref="System.ArgumentException">An element with the same key already exists in the attributes collection.</exception>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder AddAttribute(string key, string value) {
			if (_tagBuilder.Attributes.ContainsKey(key)) {
				throw new ArgumentException("An element with the same key already exists in the attributes collection");
			}
			_tagBuilder.Attributes.Add(key, value);
			return this;
		}

		/// <summary>Method to add an attribute to the tag.</summary>
		/// <param name="attribute">The attribute to add.</param>
		/// <exception cref="System.ArgumentException">An element with the same key already exists in the attributes collection.</exception>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder AddAttribute(KeyValuePair<string, string> attribute) {
			if (_tagBuilder.Attributes.ContainsKey(attribute.Key)) {
				throw new ArgumentException("An element with the same key already exists in the attributes collection.");
			}
			_tagBuilder.Attributes.Add(attribute);
			return this;
		}

		/// <summary>Method to add a css class to the tag.</summary>
		/// <param name="class">The name of the class.</param>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder AddCssClass(string @class) {
			_tagBuilder.AddCssClass(@class);
			return this;
		}

		/// <summary>Method to generate an ID for the tag.</summary>
		/// <param name="id">The ID to generate.</param>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder GenerateId(string id) {
			_tagBuilder.GenerateId(id);
			return this;
		}

		/// <summary>Method to merge html attributes for the tag.</summary>
		/// <param name="attributes">The collection of attributes to merge.</param>
		/// <param name="replaceExisting">A boolean to replace the existing attributes. [Optional: defaults to true]</param>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder MergeAttributes(IDictionary<string, string> attributes, bool replaceExisting = true) {
			_tagBuilder.MergeAttributes(attributes, replaceExisting);
			return this;
		}

		/// <summary>Method to set the inner text of the tag.</summary>
		/// <param name="text">The inner text.</param>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder SetInnerText(string text) {
			_tagBuilder.SetInnerText(text);
			return this;
		}

		/// <summary>Method to set the inner text of the tag.</summary>
		/// <param name="text">The inner text.</param>
		/// <returns>A FluentTagBuilder object.</returns>
		public FluentTagBuilder SetInnerText(MvcHtmlString text) {
			_tagBuilder.SetInnerText(text.ToHtmlString());
			return this;
		}

		/// <summary></summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public FluentTagBuilder AppendInnerHtml(string text) {
			var builder = new StringBuilder(InnerHtml);
			builder.AppendLine(InnerHtml.HasValue() ? text : string.Format("{0}{1}", Environment.NewLine, text));
			InnerHtml = builder.ToString();
			return this;
		}

		/// <summary></summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public FluentTagBuilder AppendInnerHtml(MvcHtmlString text) {
			var builder = new StringBuilder(InnerHtml);
			builder.AppendLine(InnerHtml.HasValue()
				? text.ToHtmlString()
				: string.Format("{0}{1}", Environment.NewLine, text.ToHtmlString()));
			InnerHtml = builder.ToString();
			return this;
		}

		/// <summary>Method to render the tag.</summary>
		/// <param name="renderMode">The rendering mode for the tag. [Optional: defaults to TagRenderMode.Normal]</param>
		/// <returns>A MvcHtmlString object.</returns>
		public MvcHtmlString Render(TagRenderMode renderMode = TagRenderMode.Normal) {
			return _tagBuilder.ToString(renderMode).ToMvcString();
		}

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() {
			return _tagBuilder.ToString();
		}

		#endregion

		#endregion End Public Methods
	}
}