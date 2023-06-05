using System;
using System.Xml.Linq;

namespace WordPressXmlExportParser.Extractors
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
            var baseSiteUrlElement = channelElement.Element(siteUrlElementName);
            var baseBlogUrlElement = channelElement.Element(blogUrlElementName);
            var generatorElement = channelElement.Element("generator");

            var result = new WordPressBlog
            {
                Title = blogTitleElement.Value,
                Description = descriptionElement.Value,
                Language = languageElement.Value,
                ExportVersion = versionElement.Value
            };

            if (linkElement != null && Uri.IsWellFormedUriString(linkElement.Value, UriKind.RelativeOrAbsolute))
            {
                result.Link = new Uri(linkElement.Value);
            }
            if (exportDateElement != null && DateTime.TryParse(exportDateElement.Value, out DateTime exportDate))
            {
                result.ExportDate = exportDate;
            }
            if (baseSiteUrlElement != null && Uri.IsWellFormedUriString(baseSiteUrlElement.Value, UriKind.RelativeOrAbsolute))
            {
                result.BaseSiteUri = new Uri(baseSiteUrlElement.Value);
            }
            if (baseBlogUrlElement != null && Uri.IsWellFormedUriString(baseBlogUrlElement.Value, UriKind.RelativeOrAbsolute))
            {
                result.BaseBlogUri = new Uri(baseBlogUrlElement.Value);
            }
            if (generatorElement != null && Uri.IsWellFormedUriString(generatorElement.Value, UriKind.RelativeOrAbsolute))
            {
                result.Generator = new Uri(generatorElement.Value);
            }

            return result;
        }
    }
}
