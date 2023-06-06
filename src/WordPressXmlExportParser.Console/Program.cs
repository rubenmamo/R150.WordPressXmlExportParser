// See https://aka.ms/new-console-template for more information
using WordPressXmlExportParser;

var fileName = @"C:\Users\rubenmamo\Downloads\rubenmamo.WordPress.2023-05-30.xml";

var blog = WordPressParser.ReadFile(fileName);


Console.WriteLine("Blog Details");
Console.WriteLine($"Title: {blog.Title}");
Console.WriteLine($"Link: {blog.Link}");
Console.WriteLine($"Description: {blog.Description}");
Console.WriteLine($"Export Date: {blog.ExportDate}");
Console.WriteLine($"Export Date UTC: {blog.ExportDate.ToUniversalTime()}");
Console.WriteLine($"Language: {blog.Language}");
Console.WriteLine($"Export Version: {blog.ExportVersion}");
Console.WriteLine($"Base Site Uri: {blog.BaseSiteUri}");
Console.WriteLine($"Base Blog Uri: {blog.BaseBlogUri}");

Console.ReadKey(); 
Console.WriteLine();
Console.WriteLine("Authors:");
foreach (var author in blog.Authors)
{
    Console.WriteLine($"{author.DisplayName} [{author.UserName}]");
}

Console.ReadKey();
Console.WriteLine();
Console.WriteLine("Categories:");
foreach (var category in blog.Categories)
{
    Console.WriteLine($"{category.Name} [{category.Slug}]");
}

Console.ReadKey();
Console.WriteLine();
Console.WriteLine("Tags:");
foreach (var tag in blog.Tags)
{
    Console.WriteLine($"{tag.Name} [{tag.Slug}]");
}

Console.ReadKey();
Console.WriteLine();
Console.WriteLine("Attachments:");
foreach (var attachment in blog.Attachments)
{
    Console.WriteLine($"{attachment.Title} [{attachment.AttachmentUri}]");
}

Console.ReadKey();
Console.WriteLine();
Console.WriteLine("Pages:");
foreach (var page in blog.Pages)
{
    Console.WriteLine($"{page.Title} [{page.Slug}] created by {page.Author.DisplayName} on {page.UploadDate.ToLongDateString()}");
}
Console.ReadKey();
Console.WriteLine();
Console.WriteLine("Posts:");
foreach (var post in blog.Posts)
{
    Console.WriteLine($"{post.Title} [{post.Slug}] created by {post.Author.DisplayName} on {post.UploadDate.ToLongDateString()}");
}