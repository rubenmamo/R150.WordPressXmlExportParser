using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPressXmlExportParser.UnitTests
{
    internal class AuthorExtractorTests
    {
        [Test]
        public void ExtractsCorrectAmountOfAuthors()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors.Count.Should().Be(2);
        }

        [Test]
        public void CorrectlyExtractsAuthorId()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors[0].Id.Should().Be(1);
            authors[1].Id.Should().Be(2);
        }

        [Test]
        public void CorrectlyExtractsUserName()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors[0].UserName.Should().Be("ruben_mamo");
            authors[1].UserName.Should().Be("someuser");
        }

        [Test]
        public void CorrectlyExtractsEmail()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors[0].Email.Should().Be("ruben@mamo.com");
            authors[1].Email.Should().Be("some@user.com");
        }

        [Test]
        public void CorrectlyExtractsDisplayName()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors[0].DisplayName.Should().Be("Ruben Mamo");
            authors[1].DisplayName.Should().Be("Some User");
        }

        [Test]
        public void CorrectlyExtractsFirstName()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors[0].FirstName.Should().Be("Ruben");
            authors[1].FirstName.Should().Be("Some");
        }

        [Test]
        public void CorrectlyExtractsLastName()
        {
            var authors = AuthorExtractor.ExtractAuthors(XElement.Parse(xml));

            authors[0].LastName.Should().Be("Mamo");
            authors[1].LastName.Should().Be("User");
        }

        private readonly string xml = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<channel
	xmlns:excerpt=""http://wordpress.org/export/1.2/excerpt/""
	xmlns:content=""http://purl.org/rss/1.0/modules/content/""
	xmlns:wfw=""http://wellformedweb.org/CommentAPI/""
	xmlns:dc=""http://purl.org/dc/elements/1.1/""
	xmlns:wp=""http://wordpress.org/export/1.2/""
>
	<wp:author>
		<wp:author_id>1</wp:author_id><wp:author_login>
		<![CDATA[ruben_mamo]]></wp:author_login>
		<wp:author_email><![CDATA[ruben@mamo.com]]></wp:author_email>
		<wp:author_display_name><![CDATA[Ruben Mamo]]></wp:author_display_name>
		<wp:author_first_name><![CDATA[Ruben]]></wp:author_first_name>
		<wp:author_last_name><![CDATA[Mamo]]></wp:author_last_name>
	</wp:author>
	<wp:author>
		<wp:author_id>2</wp:author_id><wp:author_login>
		<![CDATA[someuser]]></wp:author_login>
		<wp:author_email><![CDATA[some@user.com]]></wp:author_email>
		<wp:author_display_name><![CDATA[Some User]]></wp:author_display_name>
		<wp:author_first_name><![CDATA[Some]]></wp:author_first_name>
		<wp:author_last_name><![CDATA[User]]></wp:author_last_name>
	</wp:author>
</channel>";
    }
}
