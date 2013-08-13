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
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.NameEqualsIgnoreCase("city"));
			}

			private void should_return_the_correct_number_of_attributes()
			{
				_result.Count.ShouldBeEqualTo(_expectedAttributeCount);
			}

			private void should_return_the_state_attribute()
			{
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.NameEqualsIgnoreCase("state"));
			}

			private void should_return_the_street_attribute()
			{
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
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
					should_return_the_head_tag,
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
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.Type == "body");
			}

			private void should_return_the_correct_number_of_tags()
			{
				_result.Count.ShouldBeEqualTo(_expectedTagCount);
			}

			private void should_return_the_head_tag()
			{
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.Type == "head");
			}

			private void when_asked_for_its_child_tags()
			{
				_result = HTMLParser.Parse(_html)
					.ChildTags.ToList();
			}

			private void with_a_basic_html_document()
			{
				_html = "<html><head><title>The Title</title></head><body>Hello World</body></html>";
				_expectedTagCount = 2; // head, body
			}
		}

		[TestFixture]
		public class When_asked_for_its_content
		{
			private const string TitleHtml = "The &lt;&amp;&gt; Title";
			private const string TitleText = "The <&> Title";
			private string _result;
			private HTMLTag _tag;

			[Test]
			public void Given_a_tag_that_has_content()
			{
				Test.Verify(
					with_a_tag_that_has_content,
					when_asked_for_its_content,
					should_not_return_null,
					should_return_the_content
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_content()
			{
				_result.ShouldBeEqualTo(TitleText);
			}

			private void when_asked_for_its_content()
			{
				_result = _tag.Content;
			}

			private void with_a_tag_that_has_content()
			{
				const string html = "<html><head><title>" + TitleHtml + "</title></head><body>Hello World</body></html>";
				_tag = HTMLParser.Parse(html)
					.DescendantTags
					.OfType("title")
					.First();
			}
		}

		[TestFixture]
		public class When_asked_for_its_descendant_tags
		{
			private int _expectedTagCount;
			private string _html;
			private List<HTMLTag> _result;

			[Test]
			public void Given_a_basic_html_document()
			{
				Test.Verify(
					with_a_basic_html_document,
					when_asked_for_its_descendant_tags,
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
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.Type == "body");
			}

			private void should_return_the_correct_number_of_tags()
			{
				_result.Count.ShouldBeEqualTo(_expectedTagCount);
			}

			private void should_return_the_head_tag()
			{
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.Type == "head");
			}

			private void should_return_the_title_tag()
			{
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
				_result.Single(x => x.Type == "title");
			}

			private void when_asked_for_its_descendant_tags()
			{
				_result = HTMLParser.Parse(_html)
					.DescendantTags.ToList();
			}

			private void with_a_basic_html_document()
			{
				_html = "<html><head><title>The Title</title></head><body>Hello World</body></html>";
				_expectedTagCount = 3; // head, title, body
			}
		}

		[TestFixture]
		public class When_asked_for_its_parent_tag
		{
			private HTMLTag _result;
			private HTMLTag _tag;

			[Test]
			public void Given_a_tag_that_has_a_parent()
			{
				Test.Verify(
					with_a_tag_that_has_a_parent,
					when_asked_for_its_parent_tag,
					should_not_return_null,
					should_return_the_parent_tag
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_parent_tag()
			{
				_result.Type.ShouldBeEqualTo("head");
			}

			private void when_asked_for_its_parent_tag()
			{
				_result = _tag.Parent;
			}

			private void with_a_tag_that_has_a_parent()
			{
				const string html = "<html><head><title>The Title</title></head><body>Hello World</body></html>";
				_tag = HTMLParser.Parse(html)
					.DescendantTags
					.OfType("title")
					.First();
			}
		}

		[TestFixture]
		public class When_asked_for_its_raw_content
		{
			private const string TitleHtml = "The &lt;&amp;&gt; Title";
			private string _result;
			private HTMLTag _tag;

			[Test]
			public void Given_a_tag_that_has_content()
			{
				Test.Verify(
					with_a_tag_that_has_content,
					when_asked_for_its_raw_content,
					should_not_return_null,
					should_return_the_raw_content
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_raw_content()
			{
				_result.ShouldBeEqualTo(TitleHtml);
			}

			private void when_asked_for_its_raw_content()
			{
				_result = _tag.RawContent;
			}

			private void with_a_tag_that_has_content()
			{
				const string html = "<html><head><title>" + TitleHtml + "</title></head><body>Hello World</body></html>";
				_tag = HTMLParser.Parse(html)
					.DescendantTags
					.OfType("title")
					.First();
			}
		}

		[TestFixture]
		public class When_asked_to_map_a_tag_to_an_object
		{
			private const int Age = 55;
			private const decimal Amount = 121.50m;
			private const double Average = 2.435d;
			private const bool Enabled = true;
			private const string Name = "Bob";
			private DateTime _date = new DateTime(2010, 7, 16);
			private HTMLDocument _tag;
			private Person _target;

			[Test]
			public void Given_a_tag_and_object_for_which_all_properties_can_be_mapped()
			{
				Test.Verify(
					with_a_tag_having_non_default_attribute_values,
					with_a_target_object_having_matching_property_names_and_types,
					when_asked_to_map_the_tag_to_the_target,
					should_map_all_values
					);
			}

			public class Person
			{
				public int Age { get; set; }
				public decimal Amount { get; set; }
				public double Average { get; set; }
				public DateTime Date { get; set; }
				public bool Enabled { get; set; }
				public string Name { get; set; }
			}

			private void should_map_all_values()
			{
				_target.Age.ShouldBeEqualTo(Age, "failed to map age");
				_target.Amount.ShouldBeEqualTo(Amount, "failed to map amount");
				_target.Average.ShouldBeEqualTo(Average, "failed to map average");
				_target.Date.ShouldBeEqualTo(_date, "failed to map date");
				_target.Enabled.ShouldBeEqualTo(Enabled, "failed to map enabled");
				_target.Name.ShouldBeEqualTo(Name, "failed to map name");
			}

			private void when_asked_to_map_the_tag_to_the_target()
			{
				_tag.MapTo(_target);
			}

			private void with_a_tag_having_non_default_attribute_values()
			{
				_tag = HTMLParser.Parse(String.Format("<person name='{0}' enabled='{1}' age='{2}' Amount='{3}' average='{4}' date='{5}' />",
				                                      Name,
				                                      Enabled.ToString().ToUpper(),
				                                      Age,
				                                      Amount,
				                                      Average,
				                                      _date.ToString("MM/dd/yyyy")));
			}

			private void with_a_target_object_having_matching_property_names_and_types()
			{
				_target = new Person();
			}
		}
	}
}