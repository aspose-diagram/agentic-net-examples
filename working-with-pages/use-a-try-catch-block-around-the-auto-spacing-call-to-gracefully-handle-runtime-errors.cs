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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram from the specified file.
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

        // Configure auto‑spacing options using the correct property names.
        AutoSpaceOptions options = new AutoSpaceOptions
        {
            // Example spacing values; adjust as needed (in inches).
            DistanceInHorizontal = 0.5,
            DistanceInVertical = 0.5
        };

        // Attempt to auto‑space the shapes on the page.
        try
        {
            // AutoSpaceShapes takes a ShapeCollection and AutoSpaceOptions.
            page.AutoSpaceShapes(page.Shapes, options);
            Console.WriteLine("Auto‑spacing completed successfully.");
        }
        catch (Exception ex)
        {
            // Gracefully handle any runtime errors during auto‑spacing.
            Console.Error.WriteLine($"Auto‑spacing failed: {ex.Message}");
        }

        // Save the modified diagram to the output path.
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