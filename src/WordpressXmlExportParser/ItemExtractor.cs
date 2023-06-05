using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Xml.Linq;

namespace WordPressXmlExportParser
{
    internal static class ItemExtractor
    {
        private readonly static XName postTypeElementName = WordPressParser.WordPressXmlNamespace + "post_type";
        private readonly static XName postIdElementName = WordPressParser.WordPressXmlNamespace + "post_id";
        private readonly static XName attachmentUriElementName = WordPressParser.WordPressXmlNamespace + "attachment_url";
        private readonly static XName slugElementName = WordPressParser.WordPressXmlNamespace + "post_name";
        private readonly static XName contentElementName = WordPressParser.ContentXmlNamespace + "encoded";
        private readonly static XName creatorElementName = WordPressParser.DcXmlNamespace + "creator";


        internal static void ExtractItemsAndAddToBlog(XElement channelElement, WordPressBlog blog)
        {
            var attachments = new List<Attachment>();
            var pages = new List<Page>();
            foreach (var itemElement in channelElement.Elements("item"))
            {
                ExtractItemAndAddToBlog(itemElement, blog.Authors, attachments, pages);
            }

            blog.Attachments = new ReadOnlyCollection<Attachment>(attachments);
            blog.Pages= new ReadOnlyCollection<Page>(pages);
        }

        private static void ExtractItemAndAddToBlog(XElement itemElement, ReadOnlyCollection<Author> authors, List<Attachment> attachments, List<Page> pages)
        {
            var postTypeElement = itemElement.Element(postTypeElementName);

            switch (postTypeElement.Value) 
            {
                case Attachment.PostType:
                    ExtractAttachmentAndAddToBlog(itemElement, attachments);
                    break;
                case Page.PostType:
                    ExtractPageANdAddToBlog(itemElement, pages, authors);
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

        private static void ExtractPageANdAddToBlog(XElement itemElement, List<Page> pages, ReadOnlyCollection<Author> authors)
        {
            var postIdElement = itemElement.Element(postIdElementName);
            var titleElement = itemElement.Element("title");
            var publishDateElement = itemElement.Element("pubDate");
            var slugElement = itemElement.Element(slugElementName);
            var contentElement = itemElement.Element(contentElementName);
            var creatorElement = itemElement.Element(creatorElementName);

            var page = new Page
            {
                Id = int.Parse(postIdElement.Value),
                Title = titleElement.Value,
                Slug = slugElement.Value,
                Content = contentElement.Value,
                Author = authors.SingleOrDefault(x => creatorElement != null && x.UserName.ToLowerInvariant() == creatorElement.Value.ToLowerInvariant())
            };
            if (publishDateElement != null && DateTime.TryParse(publishDateElement.Value, out DateTime publishDate))
            {
                page.UploadDate = publishDate;
            }

            pages.Add(page);
        }
    }
}
