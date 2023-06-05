using System;
using System.Collections.Generic;
using System.Text;

namespace WordPressXmlExportParser
{
    public struct Attachment
    {
        internal const string PostType = "attachment";

        public int Id { get; set; }

        public string Title { get; set; }

        public Uri AttachmentUri { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
