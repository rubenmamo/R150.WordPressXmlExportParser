using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace R150.WordPressXmlExportParser
{
    public struct Page
    {
        internal const string PostType = "page";

        public int Id { get; internal set; }

        public string Title { get; internal set; }

        public string Slug { get; internal set; }

        public string Content { get; internal set; }

        public string Excerpt { get; internal set; }

        public DateTime UploadDate { get; internal set; }

        public Author Author { get; internal set; }

        public Attachment? FeaturedImage { get; internal set; }

        public ReadOnlyCollection<PostMetaData> Metadata { get; internal set; }
    }
}
