using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page (you can modify this to target a specific page)
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and configure horizontal spacing
            // NOTE: The API provides DistanceInHorizontal; there is no HorizontalDistance property.
            // We use DistanceInHorizontal as the closest alternative.
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2; // Custom horizontal gap (in internal units)
            // Optionally configure vertical spacing as well
            options.DistanceInVertical = 2; // Custom vertical gap (optional)

            // Apply auto-spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}