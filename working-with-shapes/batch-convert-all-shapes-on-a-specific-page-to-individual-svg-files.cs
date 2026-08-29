using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class BatchShapeToSvg
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourceFile = @"C:\Diagrams\input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourceFile);

            // Index of the page whose shapes will be exported (0‑based)
            int pageIndex = 0;

            // Ensure the page index is valid
            if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
            {
                Console.WriteLine("Invalid page index.");
                return;
            }

            // Get the target page
            Page page = diagram.Pages[pageIndex];

            // Directory where individual SVG files will be saved
            string outputDir = @"C:\Diagrams\ShapeSvgs";
            Directory.CreateDirectory(outputDir);

            // Iterate through all shapes on the page
            int shapeCounter = 0;
            foreach (Shape shape in page.Shapes)
            {
                // Build a unique file name for each shape
                string svgFileName = Path.Combine(
                    outputDir,
                    $"shape_{shape.ID}_{shapeCounter}.svg");

                // Create SVG save options (default options are sufficient for most cases)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to an SVG file
                shape.ToSvg(svgFileName, svgOptions);

                shapeCounter++;
            }

            Console.WriteLine($"Exported {shapeCounter} shapes from page {pageIndex} to SVG files.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
