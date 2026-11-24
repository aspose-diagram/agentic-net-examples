using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file (required) and output HTML file (optional).
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "triangle.html";

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0).
                Page page = diagram.Pages[0];

                // Define points that form a triangle.
                // The points are provided as a flat double array: x1, y1, x2, y2, ...
                double[] trianglePoints = new double[]
                {
                    1.0, 1.0,   // Point 1
                    5.0, 1.0,   // Point 2
                    3.0, 4.0,   // Point 3
                    1.0, 1.0    // Close the polygon by repeating the first point
                };

                // Draw the triangle polyline on the page.
                // The method returns the shape ID (long), which we ignore here.
                long shapeId = page.DrawPolyline(trianglePoints);

                // Prepare SVG save options to export only the first page.
                SVGSaveOptions svgOptions = new SVGSaveOptions
                {
                    PageIndex = 0,
                    ExportGuideShapes = false,
                    IsExportComments = false,
                    SVGFitToViewPort = true
                };

                // Export the diagram to SVG using a memory stream.
                using (MemoryStream svgStream = new MemoryStream())
                {
                    diagram.Save(svgStream, svgOptions);
                    svgStream.Position = 0;
                    string svgContent = Encoding.UTF8.GetString(svgStream.ToArray());

                    // Build a simple HTML page that embeds the SVG markup inline.
                    string htmlContent = @"<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <title>Triangle Diagram</title>
</head>
<body>
" + svgContent + @"
</body>
</html>";

                    // Write the HTML content to the specified output file.
                    File.WriteAllText(outputPath, htmlContent, Encoding.UTF8);
                    Console.WriteLine($"HTML file with inline SVG saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
