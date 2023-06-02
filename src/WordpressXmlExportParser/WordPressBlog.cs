using System;

namespace WordPressXmlExportParser
{
    public class WordPressBlog
    {
		public string Title { get; set; }

		public Uri Link { get; set; }

		public string Description { get; set; }

		public DateTime ExportDate { get; set; }

		public string Language { get; set; }
        public string ExportVersion { get; internal set; }
        public Uri BaseSiteUri { get; internal set; }
        public Uri BaseBlogUri { get; internal set; }
    }
}