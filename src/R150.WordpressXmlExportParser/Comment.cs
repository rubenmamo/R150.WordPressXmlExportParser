using System;
using System.Collections.Generic;
using System.Text;

namespace R150.WordPressXmlExportParser
{
    public struct Comment
    {
        public int Id { get; internal set; }

        public string Author { get; internal set; }

        public string AuthorEmail { get; internal set; }

        public string AuthorUrl { get; internal set; }

        public string AuthorIp { get; internal set; }

        public DateTime CommentDate { get; internal set; }

        public string CommentText { get; internal set; }

        public bool IsApproved { get; internal set; }

        public string CommentType { get; internal set; }

        public int CommentParent { get; internal set; }

        public int UserId { get; internal set; }
    }
}
