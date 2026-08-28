using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Load the Visio diagram that contains the triangle shape
        Diagram diagram = new Diagram("triangle.vsdx");

        // Retrieve the first shape on the first page (assumed to be the triangle)
        Shape triangleShape = diagram.Pages[0].Shapes[0];

        // Define a temporary file path for the SVG export
        string tempSvgPath = Path.Combine(Path.GetTempPath(), "triangleShape.svg");

        // Export the shape to SVG using the provided ToSvg method and SVGSaveOptions
        SVGSaveOptions svgOptions = new SVGSaveOptions();
        triangleShape.ToSvg(tempSvgPath, svgOptions);

        // Read the generated SVG markup
        string svgMarkup = File.ReadAllText(tempSvgPath);

        // Build an HTML page that embeds the SVG markup inline
        string htmlContent = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Triangle Diagram</title>
</head>
<body>
    " + svgMarkup + @"
</body>
</html>";

        // Save the HTML page to disk
        File.WriteAllText("triangle.html", htmlContent);
    }
}
