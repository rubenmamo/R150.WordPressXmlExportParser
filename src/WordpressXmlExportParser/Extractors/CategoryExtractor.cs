using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace WordPressXmlExportParser.Extractors
{
    internal static class CategoryExtractor
    {
        private static readonly XName categoryElementName = WordPressParser.WordPressXmlNamespace + "category";
        private static readonly XName idElementName = WordPressParser.WordPressXmlNamespace + "term_id";
        private static readonly XName niceNameElementName = WordPressParser.WordPressXmlNamespace + "category_nicename";
        private static readonly XName parentCategoryElementName = WordPressParser.WordPressXmlNamespace + "category_parent";
        private static readonly XName categorElementName = WordPressParser.WordPressXmlNamespace + "cat_name";

        internal static ReadOnlyCollection<Category> ExtractCategories(XElement channelElement)
        {
            return new ReadOnlyCollection<Category>(channelElement
                .Elements(categoryElementName)
                .Select(ExtractCategory)
                .ToList());
        }

        private static Category ExtractCategory(XElement categoryElement)
        {
            var termIdElement = categoryElement.Element(idElementName);
            var niceNameElement = categoryElement.Element(niceNameElementName);
            var parentCategoryElement = categoryElement.Element(parentCategoryElementName);
            var nameElement = categoryElement.Element(categorElementName);

            return new Category
            {
                Id = int.Parse(termIdElement.Value),
                NiceName = niceNameElement.Value,
                ParentCategoryName = parentCategoryElement.Value,
                Name = nameElement.Value
            };
        }
    }
}
