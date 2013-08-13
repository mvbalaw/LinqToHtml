using System.Xml;

using FluentAssert;

using NUnit.Framework;

namespace LinqToHtml.Tests
{
	public class HTMLTagAttributeTests
	{
		[TestFixture]
		public class When_asked_for_the_attribute_name
		{
			private XmlAttribute _attribute;
			private string _expectedName;
			private string _result;

			[Test]
			public void Given_an_attribute_that_has_a_name()
			{
				Test.Verify(
					with_an_attribute_that_has_a_name,
					when_asked_for_the_attribute_name,
					should_not_return_null,
					should_return_the_attribute_name
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_attribute_name()
			{
				_result.ShouldBeEqualTo(_expectedName);
			}

			private void when_asked_for_the_attribute_name()
			{
				_result = new HTMLTagAttribute(_attribute).Name;
			}

			private void with_an_attribute_that_has_a_name()
			{
				var xmlDocument = new XmlDocument();
				xmlDocument.LoadXml("<node datetime='now' />");
// ReSharper disable once PossibleNullReferenceException
				_attribute = xmlDocument.FirstChild.Attributes[0];
				_expectedName = "datetime";
			}
		}

		[TestFixture]
		public class When_asked_for_the_attribute_value
		{
			private XmlAttribute _attribute;
			private string _expectedValue;
			private string _result;

			[Test]
			public void Given_an_attribute_that_has_a_value()
			{
				Test.Verify(
					with_an_attribute_that_has_a_value,
					when_asked_for_the_attribute_value,
					should_not_return_null,
					should_return_the_attribute_value
					);
			}

			private void should_not_return_null()
			{
				_result.ShouldNotBeNull();
			}

			private void should_return_the_attribute_value()
			{
				_result.ShouldBeEqualTo(_expectedValue);
			}

			private void when_asked_for_the_attribute_value()
			{
				_result = new HTMLTagAttribute(_attribute).Value;
			}

			private void with_an_attribute_that_has_a_value()
			{
				var xmlDocument = new XmlDocument();
				xmlDocument.LoadXml("<node datetime='now' />");
// ReSharper disable once PossibleNullReferenceException
				_attribute = xmlDocument.FirstChild.Attributes[0];
				_expectedValue = "now";
			}
		}
	}
}