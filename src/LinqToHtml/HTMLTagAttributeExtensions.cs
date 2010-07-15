using System;

namespace LinqToHtml
{
	public static class HTMLTagAttributeExtensions
	{
		public static bool NameEqualsIgnoreCase(this HTMLTagAttribute attribute, string name)
		{
			return String.Compare(attribute.Name, name, true) == 0;
		}
	}
}