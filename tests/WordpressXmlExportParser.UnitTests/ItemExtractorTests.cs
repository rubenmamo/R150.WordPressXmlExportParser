namespace WordPressXmlExportParser.UnitTests
{
    [TestFixture]
    public class ItemExtractorTests
    {
        [Test]
        public void UnsupportedTypesDoNotCreateRecords()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(unsupportedTypeXml), blog);

            blog.Attachments.Count.Should().Be(0);
            blog.Pages.Count.Should().Be(0);
        }

        #region Attachments
        [Test]
        public void ExtractsCorrectAmountOfAttachments()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(attachmentsXml), blog);

            blog.Attachments.Count.Should().Be(2);
        }

        [Test]
        public void CorrectlyExtractsAttachmentId()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(attachmentsXml), blog);

            blog.Attachments[0].Id.Should().Be(38);
            blog.Attachments[1].Id.Should().Be(39);
        }

        [Test]
        public void CorrectlyExtractsAttachmentTitle()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(attachmentsXml), blog);

            blog.Attachments[0].Title.Should().Be("Some Image Title");
            blog.Attachments[1].Title.Should().Be("Some Other Image");
        }

        [Test]
        public void CorrectlyExtractsAttachmentUri()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(attachmentsXml), blog);

            blog.Attachments[0].AttachmentUri.Should().Be(new Uri("https://site.com/image.png"));
            blog.Attachments[1].AttachmentUri.Should().Be(new Uri("https://site.com/otherImage.png"));
        }

        [Test]
        public void CorrectlyExtractsAttachmentUploadDate()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(attachmentsXml), blog);

            blog.Attachments[0].UploadDate.Should().Be(new DateTime(2018, 11, 13, 22, 55, 32, DateTimeKind.Utc).ToLocalTime());
            blog.Attachments[1].UploadDate.Should().Be(new DateTime(2018, 11, 13, 23, 55, 32, DateTimeKind.Utc).ToLocalTime());
        }
        #endregion

        #region Pages
        [Test]
        public void ExtractsCorrectAmountOfPages()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages.Count.Should().Be(2);
        }

        [Test]
        public void CorrectlyExtractsPageId()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages[0].Id.Should().Be(5);
            blog.Pages[1].Id.Should().Be(6);
        }

        [Test]
        public void CorrectlyExtractsPageTitle()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages[0].Title.Should().Be("About");
            blog.Pages[1].Title.Should().Be("About Us");
        }

        [Test]
        public void CorrectlyExtractsPageSlug()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages[0].Slug.Should().Be("about-me");
            blog.Pages[1].Slug.Should().Be("about-us");
        }

        [Test]
        public void CorrectlyExtractsPageUploadDate()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages[0].UploadDate.Should().Be(new DateTime(2018, 11, 2, 13, 8, 43, DateTimeKind.Utc).ToLocalTime());
            blog.Pages[1].UploadDate.Should().Be(new DateTime(2018, 11, 2, 13, 8, 43, DateTimeKind.Utc).ToLocalTime());
        }

        [Test]
        public void CorrectlyExtractsPageContent()
        {
            var blog = new WordPressBlog();
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages[0].Content.Should().Be("Hello World");
            blog.Pages[1].Content.Should().Be("About Us");
        }

        [Test]
        public void CorrectlyExtractsPageAuthor()
        {
            var author = new Author
            {
                UserName = "username"
            };

            var blog = new WordPressBlog();
            blog.Authors = new ReadOnlyCollection<Author>(new List<Author>
            {
                author
            });
            ItemExtractor.ExtractItemsAndAddToBlog(XElement.Parse(pagesXml), blog);

            blog.Pages[0].Author.Should().Be(author);
            blog.Pages[1].Author.Should().Be(author);
        }

        #endregion

        #region XML
        private readonly string unsupportedTypeXml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<channel
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
    <item>
		<wp:post_id>38</wp:post_id>
		<title><![CDATA[Some Image Title]]></title>
		<wp:attachment_url>https://site.com/image.png</wp:attachment_url>
		<pubDate>Tue, 13 Nov 2018 22:55:32 +0000</pubDate>
		<wp:post_type><![CDATA[attachment_unsupported]]></wp:post_type>
	</item>
    <item>
		<wp:post_id>39</wp:post_id>
		<title><![CDATA[Some Other Image]]></title>
		<wp:attachment_url>https://site.com/otherImage.png</wp:attachment_url>
		<pubDate>Tue, 13 Nov 2018 23:55:32 +0000</pubDate>
		<wp:post_type><![CDATA[attachment_unsupported]]></wp:post_type>
	</item>
</channel>";

        private readonly string pagesXml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<channel
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
    <item>
		<title><![CDATA[About]]></title>
		<link>https://site.com/about/</link>
		<pubDate>Fri, 02 Nov 2018 13:08:43 +0000</pubDate>
		<dc:creator><![CDATA[username]]></dc:creator>
		<guid isPermaLink=""false"">http://site.com/?page_id=5</guid>
		<description></description>
		<content:encoded><![CDATA[Hello World]]></content:encoded>
		<excerpt:encoded><![CDATA[]]></excerpt:encoded>
		<wp:post_id>5</wp:post_id>
		<wp:post_date><![CDATA[2018-11-02 13:08:43]]></wp:post_date>
		<wp:post_date_gmt><![CDATA[2018-11-02 13:08:43]]></wp:post_date_gmt>
		<wp:post_modified><![CDATA[2018-11-18 23:48:08]]></wp:post_modified>
		<wp:post_modified_gmt><![CDATA[2018-11-18 22:48:08]]></wp:post_modified_gmt>
		<wp:comment_status><![CDATA[closed]]></wp:comment_status>
		<wp:ping_status><![CDATA[closed]]></wp:ping_status>
		<wp:post_name><![CDATA[about-me]]></wp:post_name>
		<wp:status><![CDATA[publish]]></wp:status>
		<wp:post_parent>0</wp:post_parent>
		<wp:menu_order>0</wp:menu_order>
		<wp:post_type><![CDATA[page]]></wp:post_type>
		<wp:post_password><![CDATA[]]></wp:post_password>
		<wp:is_sticky>0</wp:is_sticky>
    </item>
    <item>
		<title><![CDATA[About Us]]></title>
		<link>https://site.com/about-us/</link>
		<pubDate>Fri, 02 Nov 2018 13:08:43 +0000</pubDate>
		<dc:creator><![CDATA[username]]></dc:creator>
		<guid isPermaLink=""false"">http://site.com/?page_id=5</guid>
		<description></description>
		<content:encoded><![CDATA[About Us]]></content:encoded>
		<excerpt:encoded><![CDATA[]]></excerpt:encoded>
		<wp:post_id>6</wp:post_id>
		<wp:post_date><![CDATA[2018-11-02 13:08:43]]></wp:post_date>
		<wp:post_date_gmt><![CDATA[2018-11-02 13:08:43]]></wp:post_date_gmt>
		<wp:post_modified><![CDATA[2018-11-18 23:48:08]]></wp:post_modified>
		<wp:post_modified_gmt><![CDATA[2018-11-18 22:48:08]]></wp:post_modified_gmt>
		<wp:comment_status><![CDATA[closed]]></wp:comment_status>
		<wp:ping_status><![CDATA[closed]]></wp:ping_status>
		<wp:post_name><![CDATA[about-us]]></wp:post_name>
		<wp:status><![CDATA[publish]]></wp:status>
		<wp:post_parent>0</wp:post_parent>
		<wp:menu_order>0</wp:menu_order>
		<wp:post_type><![CDATA[page]]></wp:post_type>
		<wp:post_password><![CDATA[]]></wp:post_password>
		<wp:is_sticky>0</wp:is_sticky>
    </item>
</channel>";

        private readonly string attachmentsXml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<channel
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
    <item>
		<wp:post_id>38</wp:post_id>
		<title><![CDATA[Some Image Title]]></title>
		<wp:attachment_url>https://site.com/image.png</wp:attachment_url>
		<pubDate>Tue, 13 Nov 2018 22:55:32 +0000</pubDate>
		<wp:post_type><![CDATA[attachment]]></wp:post_type>
	</item>
    <item>
		<wp:post_id>39</wp:post_id>
		<title><![CDATA[Some Other Image]]></title>
		<wp:attachment_url>https://site.com/otherImage.png</wp:attachment_url>
		<pubDate>Tue, 13 Nov 2018 23:55:32 +0000</pubDate>
		<wp:post_type><![CDATA[attachment]]></wp:post_type>
	</item>
</channel>";
        #endregion
    }
}
