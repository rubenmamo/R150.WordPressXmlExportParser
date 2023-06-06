using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Xml.Linq;

namespace WordPressXmlExportParser.Extractors
{
    internal static class ItemExtractor
    {
        private readonly static XName postTypeElementName = WordPressParser.WordPressXmlNamespace + "post_type";
        private readonly static XName postIdElementName = WordPressParser.WordPressXmlNamespace + "post_id";
        private readonly static XName attachmentUriElementName = WordPressParser.WordPressXmlNamespace + "attachment_url";
        private readonly static XName slugElementName = WordPressParser.WordPressXmlNamespace + "post_name";
        private readonly static XName contentElementName = WordPressParser.ContentXmlNamespace + "encoded";
        private readonly static XName excerptElementName = WordPressParser.ExcerptXmlNamespace + "encoded";
        private readonly static XName creatorElementName = WordPressParser.DcXmlNamespace + "creator";
        private readonly static XName postDateElementName = WordPressParser.WordPressXmlNamespace + "post_date";
        private readonly static XName postMetaElementName = WordPressParser.WordPressXmlNamespace + "postmeta";
        private readonly static XName metaKeyElementName = WordPressParser.WordPressXmlNamespace + "meta_key";
        private readonly static XName metaValueElementName = WordPressParser.WordPressXmlNamespace + "meta_value";
        private readonly static XName commentElementName = WordPressParser.WordPressXmlNamespace + "comment";


        internal static void ExtractItemsAndAddToBlog(XElement channelElement, WordPressBlog blog)
        {
            var attachments = new List<Attachment>();
            var pages = new List<Page>();
            var posts = new List<Post>();
            foreach (var itemElement in channelElement.Elements("item").Where(e => e.Element(postTypeElementName).Value == Attachment.PostType))
            {
                ExtractItemAndAddToBlog(itemElement, blog, attachments, pages, posts);
            }
            blog.Attachments = new ReadOnlyCollection<Attachment>(attachments);

            foreach (var itemElement in channelElement.Elements("item").Where(e => e.Element(postTypeElementName).Value != Attachment.PostType))
            {
                ExtractItemAndAddToBlog(itemElement, blog, attachments, pages, posts);
            }

            blog.Pages = new ReadOnlyCollection<Page>(pages);
            blog.Posts = new ReadOnlyCollection<Post>(posts);
        }

        private static void ExtractItemAndAddToBlog(XElement itemElement, WordPressBlog blog, List<Attachment> attachments, List<Page> pages, List<Post> posts)
        {
            var postTypeElement = itemElement.Element(postTypeElementName);

            switch (postTypeElement.Value)
            {
                case Attachment.PostType:
                    ExtractAttachmentAndAddToBlog(itemElement, attachments);
                    break;
                case Page.PostType:
                    ExtractPageAndAddToBlog(itemElement, pages, blog);
                    break;
                case Post.PostType:
                    ExtractPostAndAddToBlog(itemElement, posts, blog);
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
            var publishDateElement = itemElement.Element(postDateElementName);

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

        private static void ExtractPageAndAddToBlog(XElement itemElement, List<Page> pages, WordPressBlog blog)
        {
            var postIdElement = itemElement.Element(postIdElementName);
            var titleElement = itemElement.Element("title");
            var publishDateElement = itemElement.Element(postDateElementName);
            var slugElement = itemElement.Element(slugElementName);
            var contentElement = itemElement.Element(contentElementName);
            var excerptElement = itemElement.Element(excerptElementName);
            var creatorElement = itemElement.Element(creatorElementName);
            var postMetaDataElement = itemElement.Elements(postMetaElementName);

            var postMetaData = new ReadOnlyCollection<PostMetaData>(postMetaDataElement.Select(ExtractPostMetadata).ToList());
            var page = new Page
            {
                Id = int.Parse(postIdElement.Value),
                Title = titleElement.Value,
                Slug = slugElement.Value,
                Content = contentElement.Value,
                Excerpt = excerptElement.Value,
                Author = blog.Authors.SingleOrDefault(x => creatorElement != null && x.UserName.ToLowerInvariant() == creatorElement.Value.ToLowerInvariant()),
                Metadata = postMetaData,
                FeaturedImage = ExtractFeaturedImage(postMetaData, blog)
            };
            if (publishDateElement != null && DateTime.TryParse(publishDateElement.Value, out DateTime publishDate))
            {
                page.UploadDate = publishDate;
            }

            pages.Add(page);
        }

        private static void ExtractPostAndAddToBlog(XElement itemElement, List<Post> posts, WordPressBlog blog)
        {
            var postIdElement = itemElement.Element(postIdElementName);
            var titleElement = itemElement.Element("title");
            var publishDateElement = itemElement.Element(postDateElementName);
            var slugElement = itemElement.Element(slugElementName);
            var contentElement = itemElement.Element(contentElementName);
            var excerptElement = itemElement.Element(excerptElementName);
            var creatorElement = itemElement.Element(creatorElementName);
            var postMetaDataElements = itemElement.Elements(postMetaElementName);
            var commentDataElements = itemElement.Elements(commentElementName);
            var categories = itemElement.Elements("category");

            var postMetaData = new ReadOnlyCollection<PostMetaData>(postMetaDataElements.Select(ExtractPostMetadata).ToList());
            var post = new Post
            {
                Id = int.Parse(postIdElement.Value),
                Title = titleElement.Value,
                Slug = slugElement.Value,
                Content = contentElement.Value,
                Excerpt = excerptElement.Value,
                Author = blog.Authors.SingleOrDefault(x => creatorElement != null && x.UserName.ToLowerInvariant() == creatorElement.Value.ToLowerInvariant()),
                MetaData = postMetaData,
                FeaturedImage = ExtractFeaturedImage(postMetaData, blog),
                Comments = new ReadOnlyCollection<Comment>(commentDataElements.Select(CommentExtractor.ExtractComment).ToList()),
                Categories = ExtractCategories(categories, blog),
                Tags = ExtractTags(categories, blog),
            };
            if (publishDateElement != null && DateTime.TryParse(publishDateElement.Value, out DateTime publishDate))
            {
                post.UploadDate = publishDate;
            }

            posts.Add(post);
        }

        private static PostMetaData ExtractPostMetadata(XElement itemElement)
        {
            var keyElement = itemElement.Element(metaKeyElementName);
            var valueElement = itemElement.Element(metaValueElementName);

            return new PostMetaData
            {
                Key = keyElement.Value,
                Value = valueElement.Value
            };
        }

        private static Attachment? ExtractFeaturedImage(ReadOnlyCollection<PostMetaData> postMetaData, WordPressBlog blog)
        {
            var thumbnailData = postMetaData.SingleOrDefault(x => x.Key == "_thumbnail_id");

            if (thumbnailData.Value == null)
            {
                return null;
            }

            return blog.Attachments.SingleOrDefault(x => x.Id == int.Parse(thumbnailData.Value));
        }

        private static ReadOnlyCollection<Category> ExtractCategories(IEnumerable<XElement> categoryElements, WordPressBlog blog)
        {
            var result = new List<Category>();
            foreach (var categoryElement in categoryElements)
            {
                if (categoryElement.Attribute("domain").Value == "category")
                {
                    var niceNameElement = categoryElement.Attribute("nicename");
                    result.Add(blog.Categories.SingleOrDefault(x => x.Slug == niceNameElement.Value));

                }
            }

            return new ReadOnlyCollection<Category>(result);
        }

        private static ReadOnlyCollection<Tag> ExtractTags(IEnumerable<XElement> categoryElements, WordPressBlog blog)
        {
            var result = new List<Tag>();
            foreach (var categoryElement in categoryElements)
            {
                if (categoryElement.Attribute("domain").Value == "post_tag")
                {
                    var niceNameElement = categoryElement.Attribute("nicename");
                    result.Add(blog.Tags.SingleOrDefault(x => x.Slug == niceNameElement.Value));

                }
            }

            return new ReadOnlyCollection<Tag>(result);
        }
    }
}
