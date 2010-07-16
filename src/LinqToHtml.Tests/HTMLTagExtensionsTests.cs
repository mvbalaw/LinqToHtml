using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLTagExtensionsTests
	{
		[TestFixture]
		public class When_asked_if_its_type_equals_ignore_case
		{
			private bool _result;
			private string _tagType;
			private string _type;

			[Test]
			public void Given_the_type_has_different_case()
			{
				Test.Verify(
					with_a_type_that_differs_only_by_case,
					when_asked_if_its_type_equals_ignore_case,
					should_return_true
					);
			}

			[Test]
			public void Given_the_type_is_completely_different()
			{
				Test.Verify(
					with_a_type_that_is_completely_different,
					when_asked_if_its_type_equals_ignore_case,
					should_return_false
					);
			}

			[Test]
			public void Given_the_type_matches_exactly()
			{
				Test.Verify(
					with_a_type_that_matches_exactly,
					when_asked_if_its_type_equals_ignore_case,
					should_return_true
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

			private void when_asked_if_its_type_equals_ignore_case()
			{
				_result = HTMLParser.Parse("<" + _tagType + " />")
					.TypeEqualsIgnoreCase(_type);
			}

			private void with_a_type_that_differs_only_by_case()
			{
				_type = "HeAd";
				_tagType = "head";
			}

			private void with_a_type_that_is_completely_different()
			{
				_type = "HeAd";
				_tagType = "tail";
			}

			private void with_a_type_that_matches_exactly()
			{
				_type = "HeAd";
				_tagType = _type;
			}
		}
	}
}