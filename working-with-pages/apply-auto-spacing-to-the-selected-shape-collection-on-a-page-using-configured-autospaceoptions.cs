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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [pageIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int pageIndex = 0; // default to first page

        // Optional page index argument.
        if (args.Length >= 3 && !int.TryParse(args[2], out pageIndex))
        {
            Console.Error.WriteLine("Invalid page index argument.");
            return;
        }

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the requested page index exists.
                if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Page index {pageIndex} is out of range. Diagram contains {diagram.Pages.Count} pages.");
                    return;
                }

                // Retrieve the target page.
                Page page = diagram.Pages[pageIndex];

                // Configure auto‑spacing options.
                AutoSpaceOptions options = new AutoSpaceOptions();
                // Set desired horizontal and vertical distances (in inches).
                options.DistanceInHorizontal = 0.5; // example horizontal spacing
                options.DistanceInVertical = 0.5;   // example vertical spacing

                // Apply auto‑spacing to all shapes on the page.
                // The method expects a ShapeCollection and the options instance.
                page.AutoSpaceShapes(page.Shapes, options);

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Auto‑spacing applied and diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}