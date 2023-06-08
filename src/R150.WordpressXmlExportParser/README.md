# R150.WordPressXmlExportParser
A C# class library for parsing of WordPress XML file exports.

# Build Status

[![NuGet version (Newtonsoft.Json)](https://img.shields.io/nuget/v/R150.WordPressXmlExportParser.svg?style=flat-square)](https://www.nuget.org/packages/R150.WordPressXmlExportParser/)
  <a href="https://github.com/rubenmamo/R150.WordPressXmlExportParser/actions?query=workflow%3ABuildAndTest+branch%3Amain">
    <img alt="Build states" src="https://github.com/rubenmamo/R150.WordPressXmlExportParser/workflows/BuildAndTest/badge.svg">
  </a>


  
## Parse Local Xml File

``` csharp
string fileName = "wordpressexport.xml";
var blogData = WordPressParser.ReadFile(fileName);
Console.WriteLine($"Title: {blog.Title}");
```

## Parse Local Xml File (Async)

``` csharp
string fileName = "wordpressexport.xml";
var blog = await WordPressParser.ReadFileAsync(fileName);
Console.WriteLine($"Title: {blog.Title}");
```

## Parse Xml Text

``` csharp
string xml = ""; // TODO: Write XML Here
var blog = WordPressParser.ReadXml(XDocument.Parse(xml));
Console.WriteLine($"Title: {blog.Title}");
```