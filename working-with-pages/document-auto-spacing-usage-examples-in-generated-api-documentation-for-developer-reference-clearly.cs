using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define output file path
        string outputPath = "output_auto_spaced.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram does not contain any pages.");
                return;
            }

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Configure auto‑spacing options
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
            {
                // Horizontal distance between shapes (in inches)
                DistanceInHorizontal = 0.5,
                // Vertical distance between shapes (in inches)
                DistanceInVertical = 0.5
            };

            // Apply auto‑spacing to all shapes on the page
            // The Page.AutoSpaceShapes method expects a ShapeCollection; we use the page's own collection.
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Auto‑spacing applied successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}