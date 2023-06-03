using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace WordPressXmlExportParser
{
    /*		<wp:category>
		<wp:term_id>2</wp:term_id>
		<wp:category_nicename><![CDATA[blogging]]></wp:category_nicename>
		<wp:category_parent><![CDATA[]]></wp:category_parent>
		<wp:cat_name><![CDATA[Blogging]]></wp:cat_name>
	</wp:category>*/
    internal class CategoryExtractor
    {
        internal static ReadOnlyCollection<Category> ExtractCategories(XElement channelElement)
        {
            return new ReadOnlyCollection<Category>(channelElement
                .Elements(WordPressParser.WordPressXmlNamespace + "category")
                .Select(ExtractCategory)
                .ToList());
        }

        private static Category ExtractCategory(XElement categoryElement)
        {
            var termIdElement = categoryElement.Element(WordPressParser.WordPressXmlNamespace + "term_id");
            var niceNameElement = categoryElement.Element(WordPressParser.WordPressXmlNamespace + "category_nicename");
            var parentCategoryElement = categoryElement.Element(WordPressParser.WordPressXmlNamespace + "category_parent");
            var nameElement = categoryElement.Element(WordPressParser.WordPressXmlNamespace + "cat_name");

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
