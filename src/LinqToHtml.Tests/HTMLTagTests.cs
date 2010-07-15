using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLTagTests
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
		public class When_asked_for_its_attributes
		{
			private int _expectedAttributeCount;
			private string _html;
			private List<HTMLTagAttribute> _result;

			[SetUp]
			public void BeforeEachTest()
			{
				_html = "<head";
				_expectedAttributeCount = 0;
			}

			[Test]
			public void Given_a_tag_that_has_attributes()
			{
				Test.Verify(
					with_a_tag_that_has_a_street_attribute,
					with_a_tag_that_has_a_city_attribute,
					with_a_tag_that_has_a_state_attribute,
					when_asked_for_its_attributes,
					should_not_return_null,
					should_return_the_correct_number_of_attributes,
					should_return_the_street_attribute,
					should_return_the_city_attribute,
					should_return_the_state_attribute
					);
			}

			private void AddAttribute(string name, string value)
			{
				_html += String.Format(" {0}='{1}'", name, value);
				_expectedAttributeCount++;
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_city_attribute()
			{
				_result.Single(x => x.NameEqualsIgnoreCase("city"));
			}

			private void should_return_the_correct_number_of_attributes()
			{
				_result.Count.ShouldBeEqualTo(_expectedAttributeCount);
			}

			private void should_return_the_state_attribute()
			{
				_result.Single(x => x.NameEqualsIgnoreCase("state"));
			}

			private void should_return_the_street_attribute()
			{
				_result.Single(x => x.NameEqualsIgnoreCase("street"));
			}

			private void when_asked_for_its_attributes()
			{
				var node = HTMLParser.Parse(_html + " />").Head;
				_result = node.Attributes.ToList();
			}

			private void with_a_tag_that_has_a_city_attribute()
			{
				AddAttribute("city", "Anytown");
			}

			private void with_a_tag_that_has_a_state_attribute()
			{
				AddAttribute("state", "ZY");
			}

			private void with_a_tag_that_has_a_street_attribute()
			{
				AddAttribute("street", "123 Main St");
			}
		}

		[TestFixture]
		public class When_asked_for_its_child_tags
		{
			private int _expectedTagCount;
			private string _html;
			private List<HTMLTag> _result;

			[Test]
			public void Given_a_basic_html_document()
			{
				Test.Verify(
					with_a_basic_html_document,
					when_asked_for_its_child_tags,
					should_not_return_null,
					should_not_return_an_empty_list,
					should_return_the_correct_number_of_tags,
					should_return_the_html_tag
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

			private void should_return_the_correct_number_of_tags()
			{
				_result.Count.ShouldBeEqualTo(_expectedTagCount);
			}

			private void should_return_the_html_tag()
			{
				_result.Single(x => x.Name == "html");
			}

			private void when_asked_for_its_child_tags()
			{
				_result = HTMLParser.Parse(_html)
					.ChildTags.ToList();
			}

			private void with_a_basic_html_document()
			{
				_html = "<html><head><title>The Title</title></head><body>Hello World</body></html>";
				_expectedTagCount = 1; // html
			}
		}
	}
}