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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (or modify as needed).
                Page page = diagram.Pages[0];

                // Create AutoSpaceOptions and set horizontal distance to zero.
                AutoSpaceOptions options = new AutoSpaceOptions();
                // Horizontal distance of zero removes gaps between shapes horizontally.
                options.DistanceInHorizontal = 0;
                // Keep the default vertical distance (can be left unchanged or set explicitly).
                // options.DistanceInVertical = 2; // Uncomment to set a specific vertical gap.

                // Apply auto‑spacing to all shapes on the page.
                page.AutoSpaceShapes(page.Shapes, options);

                // Save the modified diagram. Preserve the original format (VSDX used here).
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram processed and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}