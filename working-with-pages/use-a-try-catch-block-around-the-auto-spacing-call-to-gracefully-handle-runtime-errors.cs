using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram = null;

        // Load the diagram inside a try-catch to handle any loading errors.
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

        // Get the first page (you can modify this to target a specific page).
        Page page = diagram.Pages[0];

        // Configure auto-space options (horizontal and vertical distances).
        AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions
        {
            // Set distances using the correct property names.
            DistanceInHorizontal = 0.5, // Adjust as needed.
            DistanceInVertical = 0.5    // Adjust as needed.
        };

        // Perform auto-spacing within a try-catch to gracefully handle runtime errors.
        try
        {
            // Auto-space all shapes on the page using the configured options.
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            Console.WriteLine("Auto-spacing completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Auto-spacing failed: {ex.Message}");
            // Continue execution; the diagram will be saved in its current state.
        }

        // Save the modified diagram inside a try-catch to capture any saving errors.
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}