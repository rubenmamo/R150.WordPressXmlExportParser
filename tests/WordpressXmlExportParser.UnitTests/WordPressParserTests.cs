using System.Reflection;

namespace WordPressXmlExportParser.UnitTests
{
    [TestFixture]
    public class WordPressParserTests
    {
        [Test]
        public void ParsesXml()
        {
			var blog = WordPressParser.ReadXml(XDocument.Parse(xml));

			blog.Should().NotBeNull();
        }

        [Test]
        public void ParsesFile()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SampleExport.xml";

            var blog = WordPressParser.ReadFile(fileName);
            
            blog.Should().NotBeNull();
            AssertFileBlogCorrect(blog);
        }


        [Test]
        public async Task ParsesFileAsync()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SampleExport.xml";

            var blog = await WordPressParser.ReadFileAsync(fileName);

            blog.Should().NotBeNull();
            AssertFileBlogCorrect(blog);
        }

        [Test]
        public async Task ReturnsNullIfCancellationRequested()
        {
            var fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\SampleExport.xml";
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            var blog = await WordPressParser.ReadFileAsync(fileName, cancellationTokenSource.Token);

            blog.Should().BeNull();
        }

        private static void AssertFileBlogCorrect(WordPressBlog blog)
        {
            blog.Authors.Count.Should().Be(2);
            blog.Categories.Count.Should().Be(2);
            blog.Tags.Count.Should().Be(2);
            blog.Attachments.Count.Should().Be(2);
        }

        private readonly string xml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<rss
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
<channel>
	<title>Some Blog</title>
	<link>https://wordpressxmlexportparser.local</link>
	<description>Some blog description</description>
	<pubDate>Tue, 30 May 2023 07:22:20 +0000</pubDate>
	<language>en-US</language>
	<wp:wxr_version>1.2</wp:wxr_version>
	<wp:base_site_url>https://wordpressxmlexportparser.local</wp:base_site_url>
	<wp:base_blog_url>https://wordpressxmlexportparser.local/blog</wp:base_blog_url>
</channel>
</rss>";
    }
}
