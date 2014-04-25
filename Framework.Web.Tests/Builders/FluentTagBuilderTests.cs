using System.Collections.Generic;
using System.Web.Mvc;
using FluentAssertions;
using Framework.Web.Builders;
using NUnit.Framework;

namespace Framework.Web.Tests.Builders
{
	[TestFixture]
	public class FluentTagBuilderTests
	{
		#region TestCaseSources
		private static readonly object[] AddAttributeKeyValuePairTestSource =
		{
			new object[] {"div", new KeyValuePair<string, string>("style", "float: left;"), "<div style=\"float: left;\"></div>"},
			new object[] {"input", new KeyValuePair<string, string>("type", "button"), "<input type=\"button\"></input>"},
		};

		private static readonly object[] SetInnerTextMvcHtmlStringSource =
		{
			new object[] {"div", new MvcHtmlString("main-div"), "<div>main-div</div>"},
			new object[] {"input", new MvcHtmlString("input-button"), "<input>input-button</input>"},
			new object[] {"table", new MvcHtmlString("grid-table"), "<table>grid-table</table>"}
		};

		private static readonly object[] AppendInnerHtmlMvcHtmlStringSource = {};

		private static readonly object[] CanBuildComplexHtmlSource = {};
		#endregion

		[TestCase("div", "<div></div>")]
		[TestCase("body", "<body></body>")]
		[TestCase("input", "<input></input>")]
		public void ConstructorTests(string input, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(input);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		[TestCaseSource("AddAttributeKeyValuePairTestSource")]
		public void AddAttributeKeyValuePairTests(string tag, KeyValuePair<string, string> attributes, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(tag).AddAttribute(attributes);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		[TestCase(new object[] { "div", "style", "float: left;", "<div style=\"float: left;\"></div>"})]
		[TestCase(new object[] { "input", "type", "button", "<input type=\"button\"></input>" })]
		[TestCase(new object[] { "table", "cellspacing", "5", "<table cellspacing=\"5\"></table>" })]
		public void AddAttributeWithStringsTests(string tag, string key, string value, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(tag).AddAttribute(key, value);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		[TestCase(new object[] {"div", "main-div", "<div class=\"main-div\"></div>"})]
		[TestCase(new object[] {"input", "input-button", "<input class=\"input-button\"></input>"})]
		[TestCase(new object[] {"table", "grid-table", "<table class=\"grid-table\"></table>"})]
		public void AddCssClassTest(string tag, string @class, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(tag).AddCssClass(@class);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		[TestCase(new object[] { "div", "main-div", "<div id=\"main-div\"></div>" })]
		[TestCase(new object[] { "input", "input-button", "<input id=\"input-button\"></input>" })]
		[TestCase(new object[] { "table", "grid-table", "<table id=\"grid-table\"></table>" })]
		public void GenerateIdTests(string tag, string id, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(tag).GenerateId(id);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		public void MergeAttributeTests(string tag, IDictionary<string, string> attributes, bool replaceExisting,
			IDictionary<string, string> expectedResults) {
			
		}

		[TestCaseSource("SetInnerTextMvcHtmlStringSource")]
		public void SetInnerTextMvcHtmlStringTests(string tag, MvcHtmlString text, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(tag).SetInnerText(text);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		[TestCase(new object[] { "div", "main-div", "<div>main-div</div>" })]
		[TestCase(new object[] { "input", "input-button", "<input>input-button</input>" })]
		[TestCase(new object[] { "table", "grid-table", "<table>grid-table</table>" })]
		public void SetInnerTextStringTests(string tag, string text, string expectedResult) {
			// act
			var actualResult = new FluentTagBuilder(tag).SetInnerText(text);

			// assert
			actualResult.ToString().Should().Be(expectedResult);
		}

		public void AppendInnerHtmlMvcHtmlStringTests(string tag, MvcHtmlString baseHtml, MvcHtmlString additionHtml, string expectedResult) {
			
		}

		public void AppendInnerHtmlStringTests(string tag, string baseHtml, string additionHtml, string expectedResult)
		{

		}

		public void CanBuildComplexHtmlTest(string tag, IEnumerable<string> complexHtml, string expectedResult) {
			
		}
	}
}