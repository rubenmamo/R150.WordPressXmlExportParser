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
        }

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
    }
}
