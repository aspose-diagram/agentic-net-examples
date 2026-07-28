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

            // Directory where individual SVG files will be saved
            string outputFolder = @"C:\Diagrams\ShapeSvgs";

            // Index of the page whose shapes will be exported (0‑based)
            int pageIndex = 0;

            // Load the diagram
            Diagram diagram = new Diagram(sourceFile);

            // Ensure the output directory exists
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            // Get the requested page
            Page page = diagram.Pages[pageIndex];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Build a unique file name for each shape (using its ID)
                string svgPath = Path.Combine(outputFolder, $"Shape_{shape.ID}.svg");

                // Create default SVG save options (customize if needed)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to an individual SVG file
                shape.ToSvg(svgPath, svgOptions);
            }

            // Optional: inform that the process is complete
            Console.WriteLine("All shapes have been exported to SVG files.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
