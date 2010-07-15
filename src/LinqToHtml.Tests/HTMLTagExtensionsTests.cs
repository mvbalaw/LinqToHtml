using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLTagExtensionsTests
	{
		[TestFixture]
		public class When_asked_if_its_name_equals_ignore_case
		{
			private string _name;
			private bool _result;
			private string _tagName;

			[Test]
			public void Given_the_name_has_different_case()
			{
				Test.Verify(
					with_a_name_that_differs_only_by_case,
					when_asked_if_its_name_equals_ignore_case,
					should_return_true
					);
			}

			[Test]
			public void Given_the_name_matches_exactly()
			{
				Test.Verify(
					with_a_name_that_matches_exactly,
					when_asked_if_its_name_equals_ignore_case,
					should_return_true
					);
			}

			[Test]
			public void Given_the_name_that_is_completely_different()
			{
				Test.Verify(
					with_a_name_that_is_completely_different,
					when_asked_if_its_name_equals_ignore_case,
					should_return_false
					);
			}

			private void should_return_false()
			{
				_result.ShouldBeFalse();
			}

			private void should_return_true()
			{
				_result.ShouldBeTrue();
			}

			private void when_asked_if_its_name_equals_ignore_case()
			{
				_result = HTMLParser.Parse("<" + _tagName + " />")
					.ChildTags.First()
					.NameEqualsIgnoreCase(_name);
			}

			private void with_a_name_that_differs_only_by_case()
			{
				_name = "HeAd";
				_tagName = "head";
			}

			private void with_a_name_that_is_completely_different()
			{
				_name = "HeAd";
				_tagName = "tail";
			}

			private void with_a_name_that_matches_exactly()
			{
				_name = "HeAd";
				_tagName = _name;
			}
		}
	}
}