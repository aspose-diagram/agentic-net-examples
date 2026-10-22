using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument and optional output path as second argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Determine output path; if not provided, create a new file name in the same directory.
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "AutoSpacedOutput.vsdx");

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page to apply auto-spacing.
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions with default spacing values.
            // These defaults provide a quick layout improvement.
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2; // default horizontal distance
            options.DistanceInVertical = 2;   // default vertical distance

            // Apply auto-spacing to all shapes on the page.
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}