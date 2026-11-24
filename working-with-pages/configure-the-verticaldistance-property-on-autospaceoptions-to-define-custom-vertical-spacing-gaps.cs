using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
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

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Retrieve the first page (you can choose a different page if needed).
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and configure a custom vertical spacing gap.
            AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
            // Set the vertical distance between shapes (units are Visio's internal units, e.g., inches).
            autoSpaceOptions.DistanceInVertical = 2; // Custom vertical spacing value.

            // Optionally, you can also set horizontal spacing:
            // autoSpaceOptions.DistanceInHorizontal = 1;

            // Apply auto‑spacing to all shapes on the page using the configured options.
            page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}