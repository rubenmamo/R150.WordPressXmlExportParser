using System;
using System.Xml.Linq;

namespace WordPressXmlExportParser
{
    internal static class WordPressBlogExtractor
    {
        private static readonly XName versionElementName = WordPressParser.WordPressXmlNamespace + "wxr_version";
        private static readonly XName siteUrlElementName = WordPressParser.WordPressXmlNamespace + "base_site_url";
        private static readonly XName blogUrlElementName = WordPressParser.WordPressXmlNamespace + "base_blog_url";

        internal static WordPressBlog ExtractWordPressBlog(XElement channelElement)
        {
            var blogTitleElement = channelElement.Element("title");
            var linkElement = channelElement.Element("link");
            var descriptionElement = channelElement.Element("description");
            var exportDateElement = channelElement.Element("pubDate");
            var languageElement = channelElement.Element("language");
            var versionElement = channelElement.Element(versionElementName);
            var baseSiteUrl = channelElement.Element(siteUrlElementName);
            var baseBlogUrl = channelElement.Element(blogUrlElementName);

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
