using System;
using System.Xml.Linq;

namespace WordPressXmlExportParser
{
    internal static class WordPressBlogExtractor
    {
        internal static WordPressBlog ExtractWordPressBlog(XElement channelElement)
        {
            var blogTitleElement = channelElement.Element("title");
            var linkElement = channelElement.Element("link");
            var descriptionElement = channelElement.Element("description");
            var exportDateElement = channelElement.Element("pubDate");
            var languageElement = channelElement.Element("language");
            var versionElement = channelElement.Element(WordPressParser.WordPressXmlNamespace + "wxr_version");
            var baseSiteUrl = channelElement.Element(WordPressParser.WordPressXmlNamespace + "base_site_url");
            var baseBlogUrl = channelElement.Element(WordPressParser.WordPressXmlNamespace + "base_blog_url");

            var result = new WordPressBlog
            {
                Title = blogTitleElement.Value,
                Description = descriptionElement.Value,
                Language = languageElement.Value,
                ExportVersion = versionElement.Value
            };

            if (Uri.IsWellFormedUriString(linkElement.Value, UriKind.RelativeOrAbsolute))
            {
                result.Link = new Uri(linkElement.Value);
            }
            if (DateTime.TryParse(exportDateElement.Value, out DateTime exportDate))
            {
                result.ExportDate = exportDate;
            }
            if (Uri.IsWellFormedUriString(baseSiteUrl.Value, UriKind.RelativeOrAbsolute))
            {
                result.BaseSiteUri = new Uri(baseSiteUrl.Value);
            }
            if (Uri.IsWellFormedUriString(baseBlogUrl.Value, UriKind.RelativeOrAbsolute))
            {
                result.BaseBlogUri = new Uri(baseBlogUrl.Value);
            }

            return result;
        }
    }
}
