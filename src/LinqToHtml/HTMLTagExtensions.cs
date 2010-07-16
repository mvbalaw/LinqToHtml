using System;
using System.Linq;

namespace LinqToHtml
{
	public static class HTMLTagExtensions
	{
		public static bool HasAttribute(this HTMLTag htmlTag, Func<HTMLTagAttribute, bool> matchRule)
		{
			return htmlTag.Attributes.Any(matchRule);
		}

		public static bool TypeEqualsIgnoreCase(this HTMLTag htmlTag, string name)
		{
			return String.Equals(htmlTag.Type, name, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}