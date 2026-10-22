using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        Diagram diagram = null;

        // Load the Visio diagram inside a try/catch to capture loading errors.
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram contains at least one page.
        if (diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("The diagram does not contain any pages.");
            return;
        }

        // Work with the first page (index 0).
        Page page = diagram.Pages[0];

        // Prepare auto‑spacing options.
        AutoSpaceOptions options = new AutoSpaceOptions
        {
            // Set horizontal and vertical distances (in inches).
            DistanceInHorizontal = 0.5,
            DistanceInVertical = 0.5
        };

        // Perform auto‑spacing with exception handling.
        try
        {
            // Auto‑space all shapes on the page.
            page.AutoSpaceShapes(page.Shapes, options);
            Console.WriteLine("Auto‑spacing completed successfully.");
        }
        catch (Exception ex)
        {
            // Log the exception details for troubleshooting.
            Console.Error.WriteLine($"Auto‑spacing failed: {ex.Message}");
        }

        // Save the modified diagram, also wrapped in try/catch.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}