using System.Collections.Generic;
using System.Linq;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLDocumentTests
	{
		public class Person
		{
			public int Age { get; set; }
			public char Gender { get; set; }
			public string Given { get; set; }
			public string Surname { get; set; }

			public override string ToString()
			{
				return Given + " " + Surname + " Age: " + Age + " Gender: " + Gender;
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
				_result.TypeEqualsIgnoreCase("BODY").ShouldBeTrue();
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
				_result.TypeEqualsIgnoreCase("HEAD").ShouldBeTrue();
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

		[TestFixture]
		public class When_asked_to_parse_XML
		{
			[Test]
			public void Should_parse_the_content_correctly()
			{
				const string html = @"<?xml version='1.0' encoding='utf-8' ?>
<People>
  <Person>
    <FirstName>Plácido</FirstName>
    <LastName>Domingo</LastName>
    <Age>69</Age>
    <Gender>M</Gender>
  </Person>
  <Person>
    <FirstName>Enrico</FirstName>
    <LastName>Caruso</LastName>
    <Age>48</Age>
    <Gender>M</Gender>
  </Person>
  <Person>
    <FirstName>Μαρία</FirstName>
    <LastName>Κάλλας</LastName>
    <Age>53</Age>
    <Gender>F</Gender>
  </Person>
</People>
";
				var doc = HTMLParser.Parse(html);
				var persons = doc
					.OfType("Person")
					.Select(x => new Person
					{
						Given = (string)x["FirstName"],
						Surname = (string)x["LastName"],
						Age = (int)x["Age"],
						Gender = (char)x["Gender"],
					})
					.ToList();

				persons.Count.ShouldBeEqualTo(3, "found incorrect number of persons");
				var kάλλας = persons.Last();
				kάλλας.Age.ShouldBeEqualTo(53);
				kάλλας.Gender.ShouldBeEqualTo('F');
				kάλλας.Given.ShouldBeEqualTo("Μαρία");
				kάλλας.Surname.ShouldBeEqualTo("Κάλλας");
			}
		}
	}
}