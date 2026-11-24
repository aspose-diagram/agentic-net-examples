using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the input Visio file as an argument.");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Create AutoSpaceOptions with default spacing values.
            // These defaults (2 units) provide a quick layout improvement.
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            autoSpaceOptions.DistanceInHorizontal = 2.0; // Horizontal spacing
            autoSpaceOptions.DistanceInVertical = 2.0;   // Vertical spacing

            // Apply auto-spacing to each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Auto-space all shapes on the current page using the options.
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);
            }

            // Define output path (same directory, with a suffix).
            string outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath) ?? "",
                Path.GetFileNameWithoutExtension(inputPath) + "_autospace.vsdx");

            // Save the updated diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with auto-spaced shapes to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}