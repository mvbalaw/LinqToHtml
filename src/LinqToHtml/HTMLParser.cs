using System.Xml;

namespace LinqToHtml
{
	public static class HTMLParser
	{
		public static HTMLDocument Parse(string html)
		{
			var xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(html);

			return new HTMLDocument(xmlDocument);
		}
	}
}