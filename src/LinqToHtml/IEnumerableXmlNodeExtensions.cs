using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LinqToHtml
{
	public static class IEnumerableXmlNodeExtensions
	{
		public static IEnumerable<XmlNode> Flatten(this IEnumerable<XmlNode> xmlNodes)
		{
			var nodesWithChildren = xmlNodes.Where(x => x.HasChildNodes).ToList();

			if (!nodesWithChildren.Any())
			{
				return nodesWithChildren;
			}

			var childNodes = nodesWithChildren.SelectMany(x => x.ChildXmlNodes()).ToList();
			nodesWithChildren.AddRange(childNodes.Flatten());
			return nodesWithChildren;
		}
	}
}