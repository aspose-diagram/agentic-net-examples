using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input diagram path and output diagram path.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [verticalDistance]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Optional vertical distance argument; default to 2.0 if not provided.
        double verticalDistance = 2.0;
        if (args.Length >= 3 && double.TryParse(args[2], out double parsedDistance))
        {
            verticalDistance = parsedDistance;
        }

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page (you can modify this to target a specific page).
            Page page = diagram.Pages[0];

            // Create AutoSpaceOptions and set the custom vertical spacing.
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInVertical = verticalDistance; // Configure vertical gap.

            // Optionally, you can also set horizontal spacing if needed:
            // options.DistanceInHorizontal = 1.0;

            // Apply auto-spacing to all shapes on the page.
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}