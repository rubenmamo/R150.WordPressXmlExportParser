using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace WordPressXmlExportParser
{
    internal static class ItemExtractor
    {
        private readonly static XName postTypeElementName = WordPressParser.WordPressXmlNamespace + "post_type";
        private readonly static XName postIdElementName = WordPressParser.WordPressXmlNamespace + "post_id";
        private readonly static XName attachmentUriElementName = WordPressParser.WordPressXmlNamespace + "attachment_url";


        internal static void ExtractItemsAndAddToBlog(XElement channelElement, WordPressBlog blog)
        {
            var attachments = new List<Attachment>();
            foreach (var itemElement in channelElement.Elements("item"))
            {
                ExtractItemAndAddToBlog(itemElement, attachments);
            }

            blog.Attachments = new ReadOnlyCollection<Attachment>(attachments);
        }

        private static void ExtractItemAndAddToBlog(XElement itemElement, List<Attachment> attachments)
        {
            var postTypeElement = itemElement.Element(postTypeElementName);

            switch (postTypeElement.Value) 
            {
                case Attachment.PostType:
                    ExtractAttachmentAndAddToBlog(itemElement, attachments);
                    break;
                default:
                    break; 
            }
        }

        private static void ExtractAttachmentAndAddToBlog(XElement itemElement, List<Attachment> attachments)
        {
            var postIdElement = itemElement.Element(postIdElementName);
            var titleElement = itemElement.Element("title");
            var attachmentUriElement = itemElement.Element(attachmentUriElementName);
            var publishDateElement = itemElement.Element("pubDate");

            var attachment = new Attachment
            {
                Id = int.Parse(postIdElement.Value),
                Title = titleElement.Value,
            };
            if (attachmentUriElement != null && Uri.IsWellFormedUriString(attachmentUriElement.Value, UriKind.RelativeOrAbsolute))
            {
                attachment.AttachmentUri = new Uri(attachmentUriElement.Value);
            }
            if (publishDateElement != null && DateTime.TryParse(publishDateElement.Value, out DateTime publishDate))
            {
                attachment.UploadDate = publishDate;
            }

            attachments.Add(attachment);
        }
    }
}
