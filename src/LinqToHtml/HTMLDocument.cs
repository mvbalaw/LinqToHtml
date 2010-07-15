using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LinqToHtml
{
	public class HTMLDocument : HTMLTag
	{
		public const string DefaultName = "#document";
		private HTMLTag _body;
		private bool _fetchedBody;
		private bool _fetchedHead;
		private HTMLTag _head;

		public HTMLDocument(XmlDocument document)
			: base(document)
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

					_body = source.FirstOrDefault(x => x.NameEqualsIgnoreCase("body"));
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
					_head = source.FirstOrDefault(x => x.NameEqualsIgnoreCase("head"));
					_fetchedHead = true;
				}
				return _head;
			}
		}

		private IEnumerable<HTMLTag> GetSource()
		{
			IEnumerable<HTMLTag> source = new[] { this };
			if (this.NameEqualsIgnoreCase(DefaultName))
			{
				source = ChildTags;
				if (source.Count() == 1 &&
				    source.First().NameEqualsIgnoreCase("html"))
				{
					source = source.First().ChildTags;
				}
			}
			return source;
		}
	}
}