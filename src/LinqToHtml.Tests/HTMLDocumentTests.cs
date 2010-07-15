using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLDocumentTests
	{
		[TestFixture]
		public class When_asked_for_all_descendant_tags
		{
			private int _expectedTagCount;
			private string _html;
			private List<HTMLTag> _result;

			[Test]
			public void Given_a_basic_html_document()
			{
				Test.Verify(
					with_a_basic_html_document,
					when_asked_for_all_descendant_tags,
					should_not_return_null,
					should_not_return_an_empty_list,
					should_return_the_correct_number_of_tags,
					should_return_the_head_tag,
					should_return_the_title_tag,
					should_return_the_body_tag
					);
			}

			private void should_not_return_an_empty_list()
			{
				_result.Count.ShouldNotBeEqualTo(0);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_body_tag()
			{
				_result.Single(x => x.Name == "body");
			}

			private void should_return_the_correct_number_of_tags()
			{
				_result.Count.ShouldBeEqualTo(_expectedTagCount);
			}

			private void should_return_the_head_tag()
			{
				_result.Single(x => x.Name == "head");
			}

			private void should_return_the_title_tag()
			{
				_result.Single(x => x.Name == "title");
			}

			private void when_asked_for_all_descendant_tags()
			{
				_result = HTMLParser.Parse(_html)
					.AllDescendantTags.ToList();
			}

			private void with_a_basic_html_document()
			{
				_html = "<html><head><title>The Title</title></head><body>Hello World</body></html>";
				_expectedTagCount = 4; // html, head, title, body
			}
		}

		[TestFixture]
		public class When_asked_for_the_BODY_tag
		{
			private string _html;
			private HTMLTag _result;

			[Test]
			public void Given_an_html_document_that_has_a_child_BODY_tag()
			{
				Test.Verify(
					with_a_document_that_has_a_child_BODY_tag,
					when_asked_for_the_BODY_tag,
					should_not_return_null,
					should_return_the_BODY_tag
					);
			}

			[Test]
			public void Given_only_a_BODY_tag()
			{
				Test.Verify(
					with_only_a_BODY_tag,
					when_asked_for_the_BODY_tag,
					should_not_return_null,
					should_return_the_BODY_tag
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_BODY_tag()
			{
				_result.NameEqualsIgnoreCase("BODY").ShouldBeTrue();
			}

			private void when_asked_for_the_BODY_tag()
			{
				_result = HTMLParser.Parse(_html).Body;
			}

			private void with_a_document_that_has_a_child_BODY_tag()
			{
				_html = "<html><body></body></html>";
			}

			private void with_only_a_BODY_tag()
			{
				_html = "<body></body>";
			}
		}

		[TestFixture]
		public class When_asked_for_the_HEAD_tag
		{
			private string _html;
			private HTMLTag _result;

			[Test]
			public void Given_an_html_document_that_has_a_child_HEAD_tag()
			{
				Test.Verify(
					with_a_document_that_has_a_child_HEAD_tag,
					when_asked_for_the_HEAD_tag,
					should_not_return_null,
					should_return_the_HEAD_tag
					);
			}

			[Test]
			public void Given_only_a_HEAD_tag()
			{
				Test.Verify(
					with_only_a_HEAD_tag,
					when_asked_for_the_HEAD_tag,
					should_not_return_null,
					should_return_the_HEAD_tag
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_HEAD_tag()
			{
				_result.NameEqualsIgnoreCase("HEAD").ShouldBeTrue();
			}

			private void when_asked_for_the_HEAD_tag()
			{
				_result = HTMLParser.Parse(_html).Head;
			}

			private void with_a_document_that_has_a_child_HEAD_tag()
			{
				_html = "<html><head></head></html>";
			}

			private void with_only_a_HEAD_tag()
			{
				_html = "<head></head>";
			}
		}
	}
}