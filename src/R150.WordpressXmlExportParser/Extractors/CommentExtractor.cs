using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using System.Xml.Linq;

namespace R150.WordPressXmlExportParser.Extractors
{
    internal static class CommentExtractor
    {
        private readonly static XName commentIdElementName = WordPressParser.WordPressXmlNamespace + "comment_id";
        private readonly static XName authorElementName = WordPressParser.WordPressXmlNamespace + "comment_author";
        private readonly static XName authorEmailElementName = WordPressParser.WordPressXmlNamespace + "comment_author_email";
        private readonly static XName authorUrlElementName = WordPressParser.WordPressXmlNamespace + "comment_author_url";
        private readonly static XName authorIpElementName = WordPressParser.WordPressXmlNamespace + "comment_author_IP";
        private readonly static XName commentDateElementName = WordPressParser.WordPressXmlNamespace + "comment_date";
        private readonly static XName contentElementName = WordPressParser.WordPressXmlNamespace + "comment_content";
        private readonly static XName approvedElementName = WordPressParser.WordPressXmlNamespace + "comment_approved";
        private readonly static XName typeElementName = WordPressParser.WordPressXmlNamespace + "comment_type";
        private readonly static XName parentElementName = WordPressParser.WordPressXmlNamespace + "comment_parent";
        private readonly static XName userIdElementName = WordPressParser.WordPressXmlNamespace + "comment_user_id";

        internal static Comment ExtractComment(XElement commentElement)
        {
            var commentIdElement = commentElement.Element(commentIdElementName);
            var authorElement = commentElement.Element(authorElementName);
            var authorEmailElement = commentElement.Element(authorEmailElementName);
            var authorUrlElement = commentElement.Element(authorUrlElementName);
            var authorIpElement = commentElement.Element(authorIpElementName);
            var commentDateElement = commentElement.Element(commentDateElementName);
            var contentElement = commentElement.Element(contentElementName);
            var approvedElement = commentElement.Element(approvedElementName);
            var typeElement = commentElement.Element(typeElementName);
            var parentElement = commentElement.Element(parentElementName);
            var userIdElement = commentElement.Element(userIdElementName);

            return new Comment
            {
                Id = int.Parse(commentIdElement.Value),
                Author = authorElement.Value,
                AuthorEmail = authorEmailElement.Value,
                AuthorUrl = authorUrlElement.Value,
                AuthorIp = authorIpElement.Value,
                CommentDate = DateTime.Parse(commentDateElement.Value),
                CommentText = contentElement.Value,
                IsApproved = approvedElement.Value == "1",
                CommentType = typeElement.Value,
                CommentParent = int.Parse(parentElement.Value),
                UserId = int.Parse(userIdElement.Value)
            };

        }
    }
}
