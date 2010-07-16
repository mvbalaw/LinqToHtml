using System.Collections.Generic;

namespace LinqToHtml
{
	public static class IEnumerableHTMLTagExtensions
	{
		public static HTMLTagTypeFilter OfType(this IEnumerable<HTMLTag> items, string type)
		{
			return new HTMLTagTypeFilter(items, type);
		}
	}
}