using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLParserTests
	{
		[TestFixture]
		public class When_asked_to_parse_an_HTML_string
		{
			private string _expectedType;
			private string _html;
			private HTMLTag _result;

			[Test]
			public void Given_a_basic_html_document()
			{
				Test.Verify(
					with_a_basic_html_document,
					when_asked_to_parse_the_string,
					should_not_return_null,
					should_return_an_html_document_with_the_correct_Type
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_an_html_document_with_the_correct_Type()
			{
				_result.Type.ShouldBeEqualTo(_expectedType);
			}

			private void when_asked_to_parse_the_string()
			{
				_result = HTMLParser.Parse(_html);
			}

			private void with_a_basic_html_document()
			{
				_html = "<html><head><title>The Title</title></head><body>Hello World</body></html>";
				_expectedType = "html";
			}
		}
	}
}