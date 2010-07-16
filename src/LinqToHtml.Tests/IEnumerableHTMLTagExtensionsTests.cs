using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class IEnumerableHTMLTagExtensionsTests
	{
		[TestFixture]
		public class When_asked_to_get_the_tags_of_a_certain_type
		{
			private IEnumerable<HTMLTag> _list;
			private IEnumerable<HTMLTag> _result;
			private string _type;

			[Test]
			public void Given_a_list_that_contains_the_requested_tag_type()
			{
				Test.Verify(
					with_a_list_that_contains_various_tags,
					with_a_type_that_is_in_the_list,
					when_asked_to_get_the_tags_for_the_requested_type,
					should_not_return_null,
					should_return_only_the_matching_tags
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_only_the_matching_tags()
			{
				var enumerable = _result.Select(x => x.Type).Distinct();
				enumerable.Count().ShouldBeEqualTo(1);
				enumerable.First().ShouldBeEqualTo(_type);
			}

			private void when_asked_to_get_the_tags_for_the_requested_type()
			{
				_result = _list.OfType(_type).ToList();
			}

			private void with_a_list_that_contains_various_tags()
			{
				_list = HTMLParser.Parse("<html><a/><b/><c/><d/><a/><b/><c/><d/></html>").ChildTags;
			}

			private void with_a_type_that_is_in_the_list()
			{
				_type = "b";
			}
		}
	}
}