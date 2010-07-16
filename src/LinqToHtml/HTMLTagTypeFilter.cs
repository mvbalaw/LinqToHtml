using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqToHtml
{
	public class HTMLTagTypeFilter : IEnumerable<HTMLTag>
	{
		private readonly IEnumerable<HTMLTag> _items;
		private readonly string _type;
		private StringComparison _stringComparison;

		public HTMLTagTypeFilter(IEnumerable<HTMLTag> items, string type)
		{
			_items = items;
			_type = type;
			_stringComparison = StringComparison.InvariantCulture;
		}

		public IEnumerator<HTMLTag> GetEnumerator()
		{
			var htmlTags = _items.Where(x => String.Equals(x.Type, _type, _stringComparison));
			return htmlTags.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public HTMLTagTypeFilter IgnoreCase()
		{
			_stringComparison = StringComparison.InvariantCultureIgnoreCase;
			return this;
		}
	}
}