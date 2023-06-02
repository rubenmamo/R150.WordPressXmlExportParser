using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WordPressXmlExportParser;

namespace WordPressXmlExportParser.UnitTests
{
    [TestClass]
    public class WordPressBlogExtractorTests
    {
		[TestMethod]
		public void CorrectlyParsesTitle()
		{
			var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

			blog.Title.Should().Be("Some Blog");
		}

		[TestMethod]
		public void CorrectlyParsesLink()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

			blog.Link.Should().Be(new Uri("https://wordpressxmlexportparser.local"));
        }

        [TestMethod]
        public void CorrectlyParsesDescription()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

            blog.Description.Should().Be("Some blog description");
        }

        [TestMethod]
        public void CorrectlyParsesExportDate()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));
            
            blog.ExportDate.Should().Be(new DateTime(2023,5,30,7,22,20,DateTimeKind.Utc).ToLocalTime());
        }

        [TestMethod]
        public void CorrectlyParsesLanguage()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

            blog.Language.Should().Be("en-US");
        }

        [TestMethod]
        public void CorrectlyParsesVersion()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

            blog.ExportVersion.Should().Be("1.2");
        }

        [TestMethod]
        public void CorrectlyParsesBaseSiteLink()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

            blog.BaseSiteUri.Should().Be(new Uri("https://wordpressxmlexportparser.local"));
        }

        [TestMethod]
        public void CorrectlyParsesBaseBlogLink()
        {
            var blog = WordPressBlogExtractor.ExtractWordPressBlog(XElement.Parse(xml));

            blog.BaseBlogUri.Should().Be(new Uri("https://wordpressxmlexportparser.local/blog"));
        }

        private string xml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<channel
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
	<title>Some Blog</title>
	<link>https://wordpressxmlexportparser.local</link>
	<description>Some blog description</description>
	<pubDate>Tue, 30 May 2023 07:22:20 +0000</pubDate>
	<language>en-US</language>
	<wp:wxr_version>1.2</wp:wxr_version>
	<wp:base_site_url>https://wordpressxmlexportparser.local</wp:base_site_url>
	<wp:base_blog_url>https://wordpressxmlexportparser.local/blog</wp:base_blog_url>
</channel>";
    }
}
