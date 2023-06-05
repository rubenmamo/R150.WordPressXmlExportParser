using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace WordPressXmlExportParser.Extractors
{
    internal static class TagExtractor
    {
        private static readonly XName tagElementName = WordPressParser.WordPressXmlNamespace + "tag";
        private static readonly XName idElementName = WordPressParser.WordPressXmlNamespace + "term_id";
        private static readonly XName niceNameElementName = WordPressParser.WordPressXmlNamespace + "tag_slug";
        private static readonly XName categorElementName = WordPressParser.WordPressXmlNamespace + "tag_name";

        internal static ReadOnlyCollection<Tag> ExtractTags(XElement channelElement)
        {
            return new ReadOnlyCollection<Tag>(channelElement
                .Elements(tagElementName)
                .Select(ExtractTag)
                .ToList());
        }

        private static Tag ExtractTag(XElement categoryElement)
        {
            var termIdElement = categoryElement.Element(idElementName);
            var niceNameElement = categoryElement.Element(niceNameElementName);
            var nameElement = categoryElement.Element(categorElementName);

            return new Tag
            {
                Id = int.Parse(termIdElement.Value),
                Slug = niceNameElement.Value,
                Name = nameElement.Value
            };
        }
    }
}
