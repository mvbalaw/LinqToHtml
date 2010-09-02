using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LinqToHtml
{
	public class HTMLDocument : HTMLTag
	{
		private HTMLTag _body;
		private bool _fetchedBody;
		private bool _fetchedHead;
		private HTMLTag _head;

		public HTMLDocument(XmlDocument document)
			: base(document.DocumentElement)
		{
			DocType = document.DocumentType;
		}

		public HTMLTag Body
		{
			get
			{
				if (!_fetchedBody)
				{
					var source = GetSource();

					_body = source.OfType("body").IgnoreCase().FirstOrDefault();
					_fetchedBody = true;
				}
				return _body;
			}
		}

		public XmlDocumentType DocType { get; private set; }

		public HTMLTag Head
		{
			get
			{
				if (!_fetchedHead)
				{
					var source = GetSource();
					_head = source.OfType("head").IgnoreCase().FirstOrDefault();
					_fetchedHead = true;
				}
				return _head;
			}
		}

		private IEnumerable<HTMLTag> GetSource()
		{
			IEnumerable<HTMLTag> source = new[] { this };
			if (source.Count() == 1 &&
			    source.First().TypeEqualsIgnoreCase("html"))
			{
				source = source.First().ChildTags;
			}
			return source;
		}
	}
}