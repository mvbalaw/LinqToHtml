using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLTagAttributeNameFilterTests
	{
		[TestFixture]
		public class When_asked_to_get_the_tags
		{
			private string _attributeName;
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_a_different_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_an_empty_list
					);
			}

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_the_same_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_only_the_matching_tags
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_an_empty_list()
			{
				_result.Count().ShouldBeEqualTo(0);
			}

			private void should_return_only_the_matching_tags()
			{
				_result.Count().ShouldBeEqualTo(3);
				_result.OfType("a").Count().ShouldBeEqualTo(1, "missing type a");
				_result.OfType("b").Count().ShouldBeEqualTo(1, "missing type b");
				_result.OfType("c").Count().ShouldBeEqualTo(1, "missing type c");
			}

			private void when_asked_to_get_the_tags_that_have_the_requested_attribute_name()
			{
				_result = _list.WithAttributeNamed(_attributeName).ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a key='x'/><b key='y'/><c key='z'/><d/></html>").ChildTags;
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case()
			{
				_attributeName = "KEY";
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case()
			{
				_attributeName = "key";
			}
		}

		[TestFixture]
		public class When_asked_to_get_the_tags_ignoring_case
		{
			private string _attributeName;
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_a_different_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_only_the_matching_tags_ignoring_case
					);
			}

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_the_same_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_only_the_matching_tags_ignoring_case
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_only_the_matching_tags_ignoring_case()
			{
				_result.Count().ShouldBeEqualTo(3);
				_result.OfType("a").Count().ShouldBeEqualTo(1, "missing type a");
				_result.OfType("b").Count().ShouldBeEqualTo(1, "missing type b");
				_result.OfType("c").Count().ShouldBeEqualTo(1, "missing type c");
			}

			private void when_asked_to_get_the_tags_that_have_the_requested_attribute_name()
			{
				_result = _list.WithAttributeNamed(_attributeName).IgnoreCase().ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a key='x'/><b key='y'/><c key='z'/><d/></html>").ChildTags;
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case()
			{
				_attributeName = "KEY";
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case()
			{
				_attributeName = "key";
			}
		}

		[TestFixture]
		public class When_asked_to_get_the_tags_ignoring_case_where_the_requested_attribute_has_a_particular_value
		{
			private string _attributeName;
			private string _attributeValue;
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_a_different_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case,
					with_a_matching_attribute_value,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_only_the_matching_tags
					);
			}

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_the_same_case_with_a_different_value()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case,
					with_a_non_matching_attribute_value,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_an_empty_list
					);
			}

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_the_same_case_with_a_matching_value()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case,
					with_a_matching_attribute_value,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_only_the_matching_tags
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_an_empty_list()
			{
				_result.Count().ShouldBeEqualTo(0);
			}

			private void should_return_only_the_matching_tags()
			{
				_result.Count().ShouldBeEqualTo(2);
				_result.OfType("a").Count().ShouldBeEqualTo(1, "missing type a");
				_result.OfType("c").Count().ShouldBeEqualTo(1, "missing type c");
			}

			private void when_asked_to_get_the_tags_that_have_the_requested_attribute_name()
			{
				_result = _list
					.WithAttributeNamed(_attributeName)
					.IgnoreCase()
					.HavingValue(_attributeValue)
					.ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a key='x'/><b key='y'/><c key='x'/><d/></html>").ChildTags;
			}

			private void with_a_matching_attribute_value()
			{
				_attributeValue = "x";
			}

			private void with_a_non_matching_attribute_value()
			{
				_attributeValue = "not there";
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case()
			{
				_attributeName = "KEY";
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case()
			{
				_attributeName = "key";
			}
		}

		[TestFixture]
		public class When_asked_to_get_the_tags_where_the_requested_attribute_has_a_particular_value
		{
			private string _attributeName;
			private string _attributeValue;
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_a_different_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case,
					with_a_matching_attribute_value,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_an_empty_list
					);
			}

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_the_same_case_with_a_different_value()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case,
					with_a_non_matching_attribute_value,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_an_empty_list
					);
			}

			[Test]
			public void Given_a_list_that_contains_a_tag_with_an_attribute_with_the_requested_attribute_name_with_the_same_case_with_a_matching_value()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case,
					with_a_matching_attribute_value,
					when_asked_to_get_the_tags_that_have_the_requested_attribute_name,
					should_not_return_null,
					should_return_only_the_matching_tags
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_an_empty_list()
			{
				_result.Count().ShouldBeEqualTo(0);
			}

			private void should_return_only_the_matching_tags()
			{
				_result.Count().ShouldBeEqualTo(2);
				_result.OfType("a").Count().ShouldBeEqualTo(1, "missing type a");
				_result.OfType("c").Count().ShouldBeEqualTo(1, "missing type c");
			}

			private void when_asked_to_get_the_tags_that_have_the_requested_attribute_name()
			{
				_result = _list
					.WithAttributeNamed(_attributeName)
					.HavingValue(_attributeValue)
					.ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a key='x'/><b key='y'/><c key='x'/><d/></html>").ChildTags;
			}

			private void with_a_matching_attribute_value()
			{
				_attributeValue = "x";
			}

			private void with_a_non_matching_attribute_value()
			{
				_attributeValue = "not there";
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_a_different_case()
			{
				_attributeName = "KEY";
			}

			private void with_an_attribute_name_that_is_on_a_tag_in_the_list_with_the_same_case()
			{
				_attributeName = "key";
			}
		}
	}
}