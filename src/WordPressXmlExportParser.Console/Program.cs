// See https://aka.ms/new-console-template for more information
using WordPressXmlExportParser;

var fileName = @"C:\Users\rubenmamo\Downloads\rubenmamo.WordPress.2023-05-30.xml";

var blog = WordPressParser.ReadFile(fileName);

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