using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Iterate over all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True)
                    continue;

                // Build the output file name using the shape's ID
                string outputPath = $"shape_{shape.ID}.svg";

                // Create SVG save options (customize if needed)
                SVGSaveOptions svgOptions = new SVGSaveOptions();

                // Export the shape to an SVG file
                shape.ToSvg(outputPath, svgOptions);

                Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
