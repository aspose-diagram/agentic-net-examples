using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Configure auto-space options.
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                // Set horizontal and vertical distances (in points or inches as per API).
                DistanceInHorizontal = 2.0,
                DistanceInVertical = 2.0
            };

            // Perform auto-spacing and capture any errors.
            try
            {
                page.AutoSpaceShapes(page.Shapes, options);
                Console.WriteLine("Auto-spacing completed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during auto-spacing: {ex.Message}");
                return;
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log unexpected errors (e.g., loading or saving failures).
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}