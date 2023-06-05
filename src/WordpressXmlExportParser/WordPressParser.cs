using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WordPressXmlExportParser
{
    public static class WordPressParser
    {
        internal static readonly XNamespace WordPressXmlNamespace = "http://wordpress.org/export/1.2/";

        public static WordPressBlog ReadFile(string fileName)
        {
            string xmlContent = File.ReadAllText(fileName);
            var document = XDocument.Parse(xmlContent);
            return ReadXml(document);
        }

        public static Task<WordPressBlog> ReadFileAsync(string fileName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return doReadFileAsync(fileName, cancellationToken);
        }

        public static WordPressBlog ReadXml(XDocument document)
        {
            return doReadXml(document);
        }

        private static async Task<WordPressBlog> doReadFileAsync(string fileName, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var streamReader = new StreamReader(fileName))
            {
                var xmlText = await streamReader.ReadToEndAsync();

                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                var document = XDocument.Parse(xmlText);
                return doReadXml(document);
            }
        }
 
        private static WordPressBlog doReadXml(XDocument document)
        {
            var rootElement = document.Root;
            var channelElement = rootElement.Element("channel");

            var result = WordPressBlogExtractor.ExtractWordPressBlog(channelElement);
            result.Categories = CategoryExtractor.ExtractCategories(channelElement);
            result.Authors = AuthorExtractor.ExtractAuthors(channelElement);
            result.Tags = TagExtractor.ExtractTags(channelElement);
            ItemExtractor.ExtractItemsAndAddToBlog(channelElement, result);

            return result;
        }
    }
}
