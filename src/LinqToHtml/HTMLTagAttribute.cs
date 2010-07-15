using System.Xml;

namespace LinqToHtml
{
	public class HTMLTagAttribute
	{
		private readonly XmlAttribute _xmlAttribute;

		public HTMLTagAttribute(XmlAttribute xmlAttribute)
		{
			_xmlAttribute = xmlAttribute;
		}

		public string Name
		{
			get { return _xmlAttribute.Name; }
		}

		public string Value
		{
			get { return _xmlAttribute.Value; }
		}
	}
}