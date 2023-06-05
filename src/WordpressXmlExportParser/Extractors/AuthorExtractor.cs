using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace WordPressXmlExportParser.Extractors
{
    internal static class AuthorExtractor
    {
        private static readonly XName authorTagName = WordPressParser.WordPressXmlNamespace + "author";
        private static readonly XName idElementName = WordPressParser.WordPressXmlNamespace + "author_id";
        private static readonly XName loginElementName = WordPressParser.WordPressXmlNamespace + "author_login";
        private static readonly XName emailElementName = WordPressParser.WordPressXmlNamespace + "author_email";
        private static readonly XName displayNameElementName = WordPressParser.WordPressXmlNamespace + "author_display_name";
        private static readonly XName firstNameElementName = WordPressParser.WordPressXmlNamespace + "author_first_name";
        private static readonly XName lastNameElementName = WordPressParser.WordPressXmlNamespace + "author_last_name";

        internal static ReadOnlyCollection<Author> ExtractAuthors(XElement channelElement)
        {
            return new ReadOnlyCollection<Author>(channelElement
                .Elements(authorTagName)
                .Select(ExtractAuthor)
                .ToList());
        }

        private static Author ExtractAuthor(XElement authorElement)
        {
            var idElement = authorElement.Element(idElementName);
            var userNameElement = authorElement.Element(loginElementName);
            var emailElement = authorElement.Element(emailElementName);
            var displayNameElement = authorElement.Element(displayNameElementName);
            var firstNameElement = authorElement.Element(firstNameElementName);
            var lastNameElement = authorElement.Element(lastNameElementName);

            return new Author
            {
                Id = int.Parse(idElement.Value),
                UserName = userNameElement.Value,
                Email = emailElement.Value,
                DisplayName = displayNameElement.Value,
                FirstName = firstNameElement.Value,
                LastName = lastNameElement.Value,
            };
        }
    }
}
