using R150.WordPressXmlExportParser.Extractors;

namespace R150.WordPressXmlExportParser.UnitTests.Extractors
{
    [TestFixture]
    public class TagExtractorTests
    {
        [Test]
        public void ExtractsCorrectAmountOfTags()
        {
            var tags = TagExtractor.ExtractTags(XElement.Parse(xml));

            tags.Count.Should().Be(2);
        }

        [Test]
        public void CorrectlyExtractsTermId()
        {
            var tags = TagExtractor.ExtractTags(XElement.Parse(xml));

            tags[0].Id.Should().Be(17);
            tags[1].Id.Should().Be(33);
        }

        [Test]
        public void CorrectlyExtractsSlug()
        {
            var tags = TagExtractor.ExtractTags(XElement.Parse(xml));

            tags[0].Slug.Should().Be("net-core");
            tags[1].Slug.Should().Be("android");
        }

        [Test]
        public void CorrectlyExtractsName()
        {
            var tags = TagExtractor.ExtractTags(XElement.Parse(xml));

            tags[0].Name.Should().Be(".NET Core");
            tags[1].Name.Should().Be("Android");
        }

        private readonly string xml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<channel
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
	<wp:tag>
		<wp:term_id>17</wp:term_id>
		<wp:tag_slug><![CDATA[net-core]]></wp:tag_slug>
		<wp:tag_name><![CDATA[.NET Core]]></wp:tag_name>
	</wp:tag>
		<wp:tag>
		<wp:term_id>33</wp:term_id>
		<wp:tag_slug><![CDATA[android]]></wp:tag_slug>
		<wp:tag_name><![CDATA[Android]]></wp:tag_name>
	</wp:tag>
</channel>";
    }
}
