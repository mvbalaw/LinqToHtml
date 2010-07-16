using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqToHtml
{
	public class HTMLTagAttributeNameFilter : IEnumerable<HTMLTag>
	{
		private readonly string _attributeName;
		private readonly IEnumerable<HTMLTag> _items;
		private StringComparison _stringComparison;

		public HTMLTagAttributeNameFilter(IEnumerable<HTMLTag> items, string attributeName)
		{
			_items = items;
			_attributeName = attributeName;
			_stringComparison = StringComparison.InvariantCulture;
		}

		public IEnumerator<HTMLTag> GetEnumerator()
		{
			var htmlTags = _items.Where(x => HasMatchingAttribute(x, a => true));
			return htmlTags.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private bool HasMatchingAttribute(HTMLTagAttribute attribute, Func<HTMLTagAttribute, bool> valueMatches)
		{
			return String.Equals(attribute.Name, _attributeName, _stringComparison) &&
			       valueMatches(attribute);
		}

		private bool HasMatchingAttribute(HTMLTag htmlTag, Func<HTMLTagAttribute, bool> valueMatches)
		{
			return htmlTag.Attributes.Any(name => HasMatchingAttribute(name, valueMatches));
		}

		public IEnumerable<HTMLTag> HavingValue(string value)
		{
			return _items.Where(x => HasMatchingAttribute(x, a => a.Value == value));
		}

		public HTMLTagAttributeNameFilter IgnoreCase()
		{
			_stringComparison = StringComparison.InvariantCultureIgnoreCase;
			return this;
		}
	}
}