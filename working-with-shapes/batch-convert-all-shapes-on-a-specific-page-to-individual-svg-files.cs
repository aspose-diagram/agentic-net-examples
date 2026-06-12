using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class ShapeToSvgBatch
{
    static void Main()
    {
        try
        {

            // Input Visio file
            string inputFile = @"C:\Diagrams\sample.vsdx";

            // Output folder for SVG files
            string outputFolder = @"C:\Diagrams\ShapesSvg";

            // Ensure the output directory exists
            Directory.CreateDirectory(outputFolder);

            // Index of the page whose shapes will be exported (0‑based)
            int pageIndex = 0;

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputFile);

            // Get the specified page
            Page page = diagram.Pages[pageIndex];

            // Options for SVG rendering (customize as needed)
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                // Example: save images used in shapes as separate files
                IsSavingImageSeparately = true,
                // Fit each SVG to its viewport
                SVGFitToViewPort = true
            };

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Build a unique file name for each shape (using its ID)
                string svgFile = Path.Combine(outputFolder, $"Shape_{shape.ID}.svg");

                // Save the shape as an individual SVG file
                shape.ToSvg(svgFile, svgOptions);
            }

            // Optional: inform that processing is complete
            Console.WriteLine("All shapes have been exported to SVG.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
