using System;

namespace WordPressXmlExportParser
{
    public struct Attachment
    {
        internal const string PostType = "attachment";

        public int Id { get; internal set; }

        public string Title { get; internal set; }

        public Uri AttachmentUri { get; internal set; }

        public DateTime UploadDate { get; internal set; }
    }
}
