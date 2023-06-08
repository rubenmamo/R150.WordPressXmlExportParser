using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace R150.WordPressXmlExportParser
{
    public class WordPressBlog
    {
		public string Title { get; internal set; }

		public Uri Link { get; internal set; }

		public string Description { get; internal set; }

		public DateTime ExportDate { get; internal set; }

		public string Language { get; internal set; }

        public string ExportVersion { get; internal set; }

        public Uri BaseSiteUri { get; internal set; }

        public Uri BaseBlogUri { get; internal set; }

        public Uri Generator { get; internal set; }

        public ReadOnlyCollection<Category> Categories { get; internal set; } = new ReadOnlyCollection<Category>(new List<Category>());

        public ReadOnlyCollection<Author> Authors { get; internal set; } = new ReadOnlyCollection<Author>(new List<Author>());

        public ReadOnlyCollection<Tag> Tags { get; internal set; } = new ReadOnlyCollection<Tag>(new List<Tag>());

        public ReadOnlyCollection<Attachment> Attachments { get; internal set; } = new ReadOnlyCollection<Attachment>(new List<Attachment>());

        public ReadOnlyCollection<Page> Pages { get; internal set; } = new ReadOnlyCollection<Page>(new List<Page>());

        public ReadOnlyCollection<Post> Posts { get; internal set; } = new ReadOnlyCollection<Post>(new List<Post>());
    }
}