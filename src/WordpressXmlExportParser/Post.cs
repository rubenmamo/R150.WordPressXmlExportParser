using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WordPressXmlExportParser
{
    public struct Post
    {
        internal const string PostType = "post";

        public int Id { get; internal set; }

        public string Title { get; internal set; }

        public string Slug { get; internal set; }

        public string Content { get; internal set; }

        public string Excerpt { get; internal set; }

        public DateTime UploadDate { get; internal set; }

        public Author Author { get; internal set; }

        public ReadOnlyCollection<Category> Categories { get; internal set; } 

        public ReadOnlyCollection<Tag> Tags { get; internal set; }

        public Attachment? FeaturedImage { get; internal set; }

        public ReadOnlyCollection<Comment> Comments { get; internal set;  }

        public ReadOnlyCollection<PostMetaData> MetaData { get; internal set; }
    }
}
