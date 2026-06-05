using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram that contains the triangle shape
            string diagramPath = "triangle.vsdx";

            // Load the diagram using the Diagram constructor (load rule)
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Assume the triangle is the first shape on the first page
                Shape triangleShape = diagram.Pages[0].Shapes[0];

                // Prepare SVG save options (optional, can be left default)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to a temporary SVG file (Shape.ToSvg rule)
                string tempSvgPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".svg");
                triangleShape.ToSvg(tempSvgPath, svgOptions);

                // Read the generated SVG markup
                string svgMarkup = File.ReadAllText(tempSvgPath);

                // Clean up the temporary file
                File.Delete(tempSvgPath);

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
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
