using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
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

            // Create AutoSpaceOptions and set a custom horizontal distance.
            AutoSpaceOptions options = new AutoSpaceOptions();
            // Define the horizontal gap between shapes (in Visio units, e.g., inches).
            options.DistanceInHorizontal = 2; // Adjust this value as needed.
            // Optionally, set vertical distance as well (using the same value here).
            options.DistanceInVertical = 2;

            // Apply auto-spacing to all shapes on the page using the configured options.
            page.AutoSpaceShapes(page.Shapes, options);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}