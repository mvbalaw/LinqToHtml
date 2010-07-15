using System;
using System.Linq;

namespace LinqToHtml
{
	public static class HTMLTagExtensions
	{
		public static bool NameEqualsIgnoreCase(this HTMLTag htmlTag, string name)
		{
			return String.Compare(htmlTag.Name, name, true) == 0;
		}

		public static bool HasAttribute(this HTMLTag htmlTag, Func<HTMLTagAttribute, bool> matchRule)
		{
			return htmlTag.Attributes.Any(matchRule);
		}
	}
}