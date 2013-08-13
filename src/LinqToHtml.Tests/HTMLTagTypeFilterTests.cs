using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLTagTypeFilterTests
	{
		[TestFixture]
		public class When_asked_to_get_the_tags
		{
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;
			private string _type;

			[Test]
			public void Given_a_list_that_contains_the_requested_tag_type_with_a_different_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_a_type_that_is_in_the_list_with_a_different_case,
					when_asked_to_get_the_tags_for_the_requested_type,
					should_not_return_null,
					should_return_an_empty_list
					);
			}

			[Test]
			public void Given_a_list_that_contains_the_requested_tag_type_with_the_same_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_a_type_that_is_in_the_list_with_the_same_case,
					when_asked_to_get_the_tags_for_the_requested_type,
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
				var list = _result.Select(x => x.Type).Distinct().ToList();
				list.Count.ShouldBeEqualTo(1);
				list.First().ShouldBeEqualTo(_type);
			}

			private void when_asked_to_get_the_tags_for_the_requested_type()
			{
				_result = _list.OfType(_type).ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a/><b/><c/><d/><a/><b/><c/><d/></html>").ChildTags;
			}

			private void with_a_type_that_is_in_the_list_with_a_different_case()
			{
				_type = "B";
			}

			private void with_a_type_that_is_in_the_list_with_the_same_case()
			{
				_type = "b";
			}
		}

		[TestFixture]
		public class When_asked_to_get_the_tags_ignoring_case
		{
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;
			private string _type;

			[Test]
			public void Given_a_list_that_contains_the_requested_tag_type_with_a_different_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_a_type_that_is_in_the_list_with_a_different_case,
					when_asked_to_get_the_tags_for_the_requested_type,
					should_not_return_null,
					should_return_only_the_matching_tags_ignoring_case
					);
			}

			[Test]
			public void Given_a_list_that_contains_the_requested_tag_type_with_the_same_case()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_a_type_that_is_in_the_list_with_the_same_case,
					when_asked_to_get_the_tags_for_the_requested_type,
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
				var list = _result.Select(x => x.Type).Distinct().ToList();
				list.Count.ShouldBeEqualTo(1);
				var type = list.First();
				String.Equals(type, _type, StringComparison.InvariantCultureIgnoreCase).ShouldBeTrue();
			}

			private void when_asked_to_get_the_tags_for_the_requested_type()
			{
				_result = _list.OfType(_type).IgnoreCase().ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a/><b/><c/><d/><a/><b/><c/><d/></html>").ChildTags;
			}

			private void with_a_type_that_is_in_the_list_with_a_different_case()
			{
				_type = "B";
			}

			private void with_a_type_that_is_in_the_list_with_the_same_case()
			{
				_type = "b";
			}
		}
	}
}