﻿using System;
using System.Collections.Generic;

namespace WordPressXmlExportParser
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

        public List<Category> Categories { get; internal set; } = new List<Category>();
    }
}