using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LinqToHtml
{
	public class HTMLTag
	{
		private readonly XmlNode _node;

		public HTMLTag(XmlNode node)
		{
			_node = node;
		}

		public IEnumerable<HTMLTag> AllDescendantTags
		{
			get
			{
				if (!_node.HasChildNodes)
				{
					yield break;
				}

				var xmlNodes = _node.ChildXmlNodes();
				var allXmlNodes = xmlNodes.Flatten();

				foreach (var tag in allXmlNodes.Select(item => new HTMLTag(item)))
				{
					yield return tag;
				}
			}
		}
		public IEnumerable<HTMLTagAttribute> Attributes
		{
			get
			{
				if (_node.Attributes == null)
				{
					yield break;
				}

				foreach (var attribute in _node.Attributes
					.Cast<XmlAttribute>()
					.Select(x => new HTMLTagAttribute(x)))
				{
					yield return attribute;
				}
			}
		}

		public IEnumerable<HTMLTag> ChildTags
		{
			get
			{
				if (!_node.HasChildNodes)
				{
					yield break;
				}
				var childXmlNodes = _node.ChildXmlNodes();
				foreach (var tag in childXmlNodes.Select(item => new HTMLTag(item)))
				{
					yield return tag;
				}
			}
		}

		public string Name
		{
			get { return _node.Name; }
		}
	}
}