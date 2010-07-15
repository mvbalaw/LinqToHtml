using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LinqToHtml
{
	public static class XmlNodeListExtensions
	{
		public static IEnumerable<XmlNode> ChildXmlNodes(this XmlNode parent)
		{
			var xmlNodeList = parent.ChildNodes.Cast<XmlNode>();
			return xmlNodeList;
		}
	}
}