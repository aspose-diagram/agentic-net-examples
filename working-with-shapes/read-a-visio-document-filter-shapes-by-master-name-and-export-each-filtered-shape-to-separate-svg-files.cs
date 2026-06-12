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

            // Path to the Visio file to process
            string inputPath = "input.vsdx";

            // Name of the master to filter shapes by (case‑sensitive)
            string targetMasterName = "Rectangle";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages in the document
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape is based on the desired master
                    if (shape.Master != null && shape.Master.Name == targetMasterName)
                    {
                        // Create a unique file name for each exported shape
                        string outputFile = $"shape_{shape.ID}_{Guid.NewGuid():N}.svg";

                        // Export the shape to SVG using default options
                        SVGSaveOptions svgOptions = new SVGSaveOptions();
                        shape.ToSvg(outputFile, svgOptions);

                        Console.WriteLine($"Exported shape ID {shape.ID} to {outputFile}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
