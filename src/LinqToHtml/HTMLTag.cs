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

		public string Content
		{
			get { return _node.InnerText; }
		}

		public IEnumerable<HTMLTag> DescendantTags
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

		public HTMLTag Parent
		{
			get { return new HTMLTag(_node.ParentNode); }
		}

		public string RawContent
		{
			get { return _node.InnerXml; }
		}

		public string Type
		{
			get { return _node.Name; }
		}

		public void MapTo<T>(T destination)
		{
			var properties = destination
				.GetType()
				.GetProperties()
				.Where(x => x.CanWrite)
				.ToDictionary(x => x.Name.ToLower());

			var attributes = Attributes
				.ToDictionary(x => x.Name.ToLower(), x => x.Value);

			var matches = attributes.Where(x => properties.ContainsKey(x.Key));

			foreach (var match in matches)
			{
				var property = properties[match.Key];
				PropertySetter
					.GetFor(property.PropertyType)
					.SetValue(destination, property, match.Value);
			}
		}
	}
}