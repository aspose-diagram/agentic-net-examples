using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // HTML content with inline SVG that draws a simple triangle
        string html = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Triangle Diagram</title>
</head>
<body>
    <svg width=""200"" height=""200"" viewBox=""0 0 200 200"" xmlns=""http://www.w3.org/2000/svg"">
        <polygon points=""100,20 180,180 20,180"" fill=""#ADD8E6"" stroke=""#000000"" stroke-width=""2"" />
    </svg>
</body>
</html>";

        // Write the HTML to a file
        string outputPath = "triangle.html";
        File.WriteAllText(outputPath, html);

        Console.WriteLine($"HTML file generated at: {outputPath}");
    }
}
