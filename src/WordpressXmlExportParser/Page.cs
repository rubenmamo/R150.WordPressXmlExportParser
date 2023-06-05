using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressXmlExportParser
{
    public struct Page
    {
        internal const string PostType = "page";

        public int Id { get; set; }

        public string Title { get; set; }

        public string Slug { get; set; }

        public string Content { get; set; }

        public DateTime UploadDate { get; set; }

        public Author Author { get; set; }
    }
}
